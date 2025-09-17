using DedsiNative.DedsiUsers;
using Microsoft.EntityFrameworkCore;

namespace DedsiNative.EntityFrameworkCores;

public class DedsiNativeDbContext(DbContextOptions<DedsiNativeDbContext> options) : DbContext(options)
{
    public DbSet<DedsiUser> DedsiUsers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DedsiUser>(b =>
        {
            b.ToTable("DedsiUsers");
            b.HasKey(e => e.Id);
        });
    }
}
