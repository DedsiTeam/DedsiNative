using DedsiNative.DedsiUsers;
using Microsoft.EntityFrameworkCore;

namespace DedsiNative.EntityFrameworkCores;

public class DedsiNativeDbContext(DbContextOptions<DedsiNativeDbContext> options) : DbContext(options)
{
    public DbSet<DedsiUser> DedsiUsers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder is null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DedsiNativeDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
