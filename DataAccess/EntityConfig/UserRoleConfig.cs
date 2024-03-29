using Chameleon.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.DataAccess.EntityConfig;

public class UserRoleConfig: IEntityTypeConfiguration<UsersRoles>
{
    public void Configure(EntityTypeBuilder<UsersRoles> builder)
    {
        builder.HasKey(pr => new {pr.RoleId, pr.UserId});

        builder.HasOne(pr => pr.User)
            .WithMany(p => p.Roles)
            .HasForeignKey(pr => pr.UserId);

        builder.HasOne(pr => pr.Roles)
            .WithMany(r => r.Users)
            .HasForeignKey(pr => pr.RoleId);
        
    }
}