using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.CompanySetting.DataAccess.EntityConfig;

public class AbsentConfig: IEntityTypeConfiguration<Absent>
{
    public void Configure(EntityTypeBuilder<Absent> builder)
    {
        builder.ToTable("Absent");
        
        builder.HasKey(x => x.Id).HasName("PK_Absent");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
      
        builder.HasOne(c => c.CreatedBy)
            .WithOne()
            .HasForeignKey<Absent>(c => c.CreatedById);
    }
}