using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.CompanySetting.DataAccess.EntityConfig;

public class CompanyUserConfig: IEntityTypeConfiguration<CompanyUser>
{
    public void Configure(EntityTypeBuilder<CompanyUser> builder)
    {
        builder.HasKey(x => new { x.CompanyId, x.UserId });
        builder.HasOne(cw => cw.Company)
            .WithMany(c => c.Users)
            .HasForeignKey(cw => cw.CompanyId);
    }
}