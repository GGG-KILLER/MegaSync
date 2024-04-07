using CG.Web.MegaApiClient;

namespace MegaSync.Model;

public sealed class Item
{
    public Item? Parent { get; set; }
    public required string Name { get; set; }
    public DateTime LastModified { get; set; }
    public List<Item> ChildItems { get; } = [];
    public INode? Node { get; set; }
    public string FullPath => Parent is not (null or { Name: "" }) ? Path.Combine(Parent.FullPath, Name) : Name;
}
