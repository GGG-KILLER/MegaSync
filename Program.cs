using MegaSync.CompiledModels;
using MegaSync.Components;
using MegaSync.Data;
using MegaSync.Services;
using Microsoft.EntityFrameworkCore;

// Doesn't work on this sqlite package version.
// SQLitePCL.raw.sqlite3_config(2 /*SQLITE_CONFIG_MULTITHREAD*/);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddResponseCompression();

builder.Services.AddHttpClient();
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseModel(AppDbContextModel.Instance).UseSqlite(builder.Configuration.GetConnectionString("SQLite")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddHostedService<SyncService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseResponseCompression();
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
