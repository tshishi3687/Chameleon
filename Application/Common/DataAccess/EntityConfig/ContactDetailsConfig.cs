using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.Common.DataAccess.EntityConfig;

public class ContactDetailsConfig: IEntityTypeConfiguration<ContactDetails>
{
    public void Configure(EntityTypeBuilder<ContactDetails> builder)
    {
        builder.ToTable("ContactDetails");
        builder.HasKey(x => x.Id).HasName("PK_ContactDetails");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne(c => c.Locality)
            .WithMany(l => l.ContactDetailsList);

        builder.HasOne(c => c.Country)
            .WithMany(co => co.ContactDetailsList);
    }
}