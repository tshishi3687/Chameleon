using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.HumanSetting.DataAccess.EntityConfig;

public class LocalityConfig : IEntityTypeConfiguration<Locality>
{
    public void Configure(EntityTypeBuilder<Locality> builder)
    {
        builder.ToTable("Locality");
        builder.HasKey(x => x.Id).HasName("PK_Locality");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}