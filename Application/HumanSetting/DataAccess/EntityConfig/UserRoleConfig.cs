using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.HumanSetting.DataAccess.EntityConfig;

public class UserRoleConfig: IEntityTypeConfiguration<UsersRoles>
{
    public void Configure(EntityTypeBuilder<UsersRoles> builder)
    {
        builder.HasKey(pr => new {pr.RoleId, pr.UserId});

        builder.HasOne(pr => pr.Roles)
            .WithMany(r => r.Users)
            .HasForeignKey(pr => pr.RoleId);

        builder.HasOne(pr => pr.Users)
            .WithMany(u => u.Roles) // ajuste si besoin
            .HasForeignKey(pr => pr.UserId);
    }

}