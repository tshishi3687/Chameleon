using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.HumanSetting.DataAccess.EntityConfig;

public class UserContactDetailsConfig: IEntityTypeConfiguration<UsersContactDetails>
{
    public void Configure(EntityTypeBuilder<UsersContactDetails> builder)
    {
        builder.HasKey(x => new { x.ContactDetailsId, x.UserId });
    }
}