using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.HumanSetting.DataAccess.EntityConfig;

public class IsActiveUserInCompanyConfig: IEntityTypeConfiguration<IsActiveUserInCompany>
{
    public void Configure(EntityTypeBuilder<IsActiveUserInCompany> builder)
    {
        builder.HasKey(x => new { x.CompanyId, x.UserId });
    }
}