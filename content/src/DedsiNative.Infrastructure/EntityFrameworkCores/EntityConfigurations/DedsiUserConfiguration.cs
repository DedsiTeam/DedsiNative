using DedsiNative.DedsiUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DedsiNative.EntityFrameworkCores.EntityConfigurations;

internal class DedsiUserConfiguration : IEntityTypeConfiguration<DedsiUser>
{
    public void Configure(EntityTypeBuilder<DedsiUser> builder)
    {
        builder.ToTable("AiAgents");
        builder.HasKey(e => e.Id);
    }
}
