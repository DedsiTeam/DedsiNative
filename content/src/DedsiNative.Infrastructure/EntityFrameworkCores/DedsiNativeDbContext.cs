using DedsiNative.DedsiUsers;
using Microsoft.EntityFrameworkCore;

namespace DedsiNative.EntityFrameworkCores;

public class DedsiNativeDbContext(DbContextOptions<DedsiNativeDbContext> options) : DbContext(options)
{
    public DbSet<DedsiUser> DedsiUsers { get; set; }
}