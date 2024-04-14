using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.CompanySetting.DataAccess.EntityConfig;

public class MemoryConfig : IEntityTypeConfiguration<Memory>
{
    public void Configure(EntityTypeBuilder<Memory> builder)
    {
        builder.ToTable("Memory");

        builder.HasKey(x => x.Id).HasName("PK_company");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(m => m.Title).IsRequired();
        builder.Property(m => m.Description).IsRequired();
        
        builder.HasOne(m => m.MadeBy)
            .WithOne()
            .HasForeignKey<Memory>(m => m.MadeById);
    }
}