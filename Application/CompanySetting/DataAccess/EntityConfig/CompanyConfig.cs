using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.CompanySetting.DataAccess.EntityConfig;

public class CompanyConfig : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Company");

        builder.HasKey(x => x.Id).HasName("PK_company");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name).IsRequired(); 
        builder.Property(x => x.BusinessNumber).IsRequired(); 
        builder.Property(x => x.ContactDetails).IsRequired();
        builder.Property(x => x.Tutor).IsRequired();

        builder.HasOne(c => c.Tutor)
            .WithMany(u => u.Companies);

        builder.HasMany(c => c.Users)
            .WithOne(cw => cw.Company)
            .HasForeignKey(cw => cw.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}