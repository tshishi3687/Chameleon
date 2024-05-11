using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.HumanSetting.DataAccess.EntityConfig;

public class RoleConfig: IEntityTypeConfiguration<Roles>
{
    public void Configure(EntityTypeBuilder<Roles> builder)
    {
        builder.ToTable("Roles");
        
        builder.HasKey(x => x.Id).HasName("PK_Roles");
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.HasMany(r => r.Users)
            .WithOne(p => p.Roles);
        
        builder.Property(x => x.Name).HasDefaultValue(EnumUsersRoles.CUSTOMER.ToString());
    }
}