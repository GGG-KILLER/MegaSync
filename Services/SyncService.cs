
using System.Runtime.CompilerServices;
using CG.Web.MegaApiClient;
using MegaSync.Data;
using MegaSync.Model;
using Microsoft.EntityFrameworkCore;

namespace MegaSync.Services;

public sealed class SyncService(ILogger<SyncService> logger, IServiceProvider serviceProvider) : BackgroundService
{
    private readonly MegaApiClient _client = new();
    private readonly ILogger<SyncService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _client.LoginAnonymousAsync();

        await SyncAsync(stoppingToken);
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SyncAsync(stoppingToken);
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Sync service stopped.");
        }
    }

    private async Task SyncAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        using var context = await contextFactory.CreateDbContextAsync(stoppingToken);

        foreach (var link in context.MegaLinks.Where(x => x.LastUpdated < DateTime.Now.AddHours(-1)))
        {
            try
            {
                stoppingToken.ThrowIfCancellationRequested();
                _logger.LogInformation("Executing sync for {Link}", link.Url);
                await context.LogMessages.AddAsync(new LogMessage { Timestamp = DateTime.Now, Message = $"Initiating sync for {link.Path}" });

                Directory.CreateDirectory(link.Path);

                var megaTree = ConvertToTree(await _client.GetNodesFromLinkAsync(new Uri(link.Url)));
                var fsTree = ConvertToTree(new DirectoryInfo(link.Path));

                var megaFiles = ToFileList(megaTree);
                var fsFiles = ToFileList(fsTree);

                var toDelete = fsFiles.ExceptBy(megaFiles.Select(x => x.FullPath), x => x.FullPath)
                                      .Select(x => x.FullPath)
                                      .OrderByDescending(x => x.Length);

                var notHere = megaFiles.ExceptBy(fsFiles.Select(x => x.FullPath), x => x.FullPath);
                var newerOnMega = megaFiles.IntersectBy(fsFiles.Select(x => x.FullPath), x => x.FullPath)
                                           .Where(x => x.LastModified > fsFiles.Single(y => x.FullPath == y.FullPath).LastModified);

                var toDownload = notHere.Union(newerOnMega);

                foreach (var delete in toDelete)
                {
                    await context.LogMessages.AddAsync(new LogMessage { Timestamp = DateTime.Now, Message = $"Deleting {delete}..." });

                    var fullPath = Path.Combine(link.Path, delete);

                    if (File.Exists(fullPath))
                        File.Delete(fullPath);
                    else
                        Directory.Delete(fullPath);

                    var dir = new DirectoryInfo(Path.GetDirectoryName(fullPath)!);
                    while (dir?.EnumerateFileSystemInfos().Any() is false)
                    {
                        var parent = dir.Parent;
                        dir.Delete();
                        dir = parent;
                    }
                }

                foreach (var download in toDownload.Where(x => x.Node?.Type == NodeType.File))
                {
                    await context.LogMessages.AddAsync(new LogMessage { Timestamp = DateTime.Now, Message = $"Downloading {download.FullPath}..." });

                    var file = new FileInfo(Path.Combine(link.Path, download.FullPath));
                    if (!file.Directory!.Exists)
                        file.Directory.Create();

                    var progress = new Progress<double>(x =>
                        _logger.LogInformation("Progress on donwloading {File}: {Percent}%", download, x));
                    await _client.DownloadFileAsync(download.Node, file.FullName, progress, stoppingToken);
                    await context.SaveChangesAsync(stoppingToken);
                }

                link.LastUpdated = DateTime.Now;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error syncing {Link}", link.Url);
                await context.LogMessages.AddAsync(new LogMessage { Timestamp = DateTime.Now, Message = $"""
                        Error syncing {link.Path} ({link.Url}):
                        {ex}
                        """ });
            }
        }

        await context.LogMessages.Where(x => x.Timestamp < DateTime.Today.AddDays(-3)).ExecuteDeleteAsync(stoppingToken);
        await context.SaveChangesAsync();
    }

    private static Item ConvertToTree(IEnumerable<INode> nodes)
    {
        var root = new Item { Name = "" };
        var items = nodes.ToDictionary(x => x.Id, x => new Item
        {
            Name = x.Name,
            LastModified = x.ModificationDate ?? DateTime.MinValue,
            Node = x,
        });
        var parentMap = nodes.ToDictionary(x => x.Id, x => x.ParentId);

        foreach (var node in items)
        {
            if (parentMap.TryGetValue(node.Key, out var parentId))
            {
                var parent = parentId is null ? root : items[parentId];
                node.Value.Parent = parent;
                parent.ChildItems.Add(node.Value);
            }
        }

        root = root.ChildItems.Single();
        root.Name = "";
        return root;
    }

    private static Item ConvertToTree(DirectoryInfo rootInfo)
    {
        var root = ToTree(rootInfo);
        root.Name = "";
        return root;

        static Item ToTree(DirectoryInfo directoryInfo)
        {
            var directory = new Item { Name = directoryInfo.Name };

            foreach (var item in directoryInfo.EnumerateFiles())
            {
                var file = new Item { Name = item.Name, LastModified = item.LastWriteTime, Parent = directory };
                file.Parent = directory;
                directory.ChildItems.Add(file);
            }

            foreach (var childDirInfo in directoryInfo.EnumerateDirectories())
            {
                var childDir = ToTree(childDirInfo);
                childDir.Parent = directory;
                directory.ChildItems.Add(childDir);
            }

            return directory;
        }
    }

    private static List<Item> ToFileList(Item root)
    {
        var files = new List<Item>();
        var queue = new Queue<Item>();
        queue.Enqueue(root);
        while (queue.TryDequeue(out var dir))
        {
            foreach (var child in dir.ChildItems)
            {
                if (child.ChildItems.Count != 0)
                {
                    queue.Enqueue(child);
                }
                else
                {
                    files.Add(new Item { Name = child.FullPath, LastModified = child.LastModified, Node = child.Node });
                }
            }
        }
        return files;
    }
}