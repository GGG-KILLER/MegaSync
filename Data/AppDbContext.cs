using MegaSync.Model;
using Microsoft.EntityFrameworkCore;

namespace MegaSync.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<MegaLink> MegaLinks { get; set; }
    public DbSet<LogMessage> LogMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MegaLink>()
                    .HasKey(nameof(MegaLink.Url));

        modelBuilder.Entity<MegaLink>()
                    .HasIndex(nameof(MegaLink.Path))
                    .IsUnique();

        modelBuilder.Entity<LogMessage>()
                    .HasKey(x => x.Id);
        modelBuilder.Entity<LogMessage>()
                    .Property(x => x.Id)
                    .ValueGeneratedOnAdd();

        modelBuilder.Entity<LogMessage>()
                    .HasIndex(nameof(LogMessage.Timestamp))
                    .IsDescending();
    }
}
