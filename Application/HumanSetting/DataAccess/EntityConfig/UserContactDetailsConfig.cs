using Chameleon.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.DataAccess.EntityConfig;

public class UserContactDetailsConfig: IEntityTypeConfiguration<UsersContactDetails>
{
    public void Configure(EntityTypeBuilder<UsersContactDetails> builder)
    {
        builder.HasKey(x => new { x.ContactDetailsId, x.UserId });
        builder.HasOne(uc => uc.User)
            .WithMany(u => u.ContactDetails)
            .HasForeignKey(uc => uc.UserId);
    }
}