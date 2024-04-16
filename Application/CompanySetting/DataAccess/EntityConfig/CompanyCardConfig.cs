using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.CompanySetting.DataAccess.EntityConfig;

public class CompanyCardConfig : IEntityTypeConfiguration<CompanyCard>
{
    public void Configure(EntityTypeBuilder<CompanyCard> builder)
    {
        builder.HasKey(x => new { x.CompanyGuid, x.CardGuid });
    }
}