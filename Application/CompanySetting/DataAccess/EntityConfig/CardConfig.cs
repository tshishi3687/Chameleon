using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.CompanySetting.DataAccess.EntityConfig;

public class CardConfig : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("Card");

        builder.HasKey(x => x.Id).HasName("PK_company");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(c => c.DateTime).IsRequired();
        builder.Property(c => c.CompanyIGuid).IsRequired();

        builder.HasOne(c => c.AbsentDetails)
            .WithOne()
            .HasForeignKey<Card>(c => c.AbsentDetailsId);

        builder.HasOne(c => c.MemoryDetails)
            .WithOne()
            .HasForeignKey<Card>(c => c.MemoryDetailsId);

        builder.HasOne(c => c.TaskOrEventDetails)
            .WithOne()
            .HasForeignKey<Card>(c => c.TaskOrEventDetailsId);
    }
}