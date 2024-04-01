using Chameleon.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.DataAccess.EntityConfig;

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