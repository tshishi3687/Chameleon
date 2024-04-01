using Chameleon.DataAccess.Entity;
using Chameleon.Securities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.DataAccess.EntityConfig;

public class RoleConfig: IEntityTypeConfiguration<Roles>
{
    public void Configure(EntityTypeBuilder<Roles> builder)
    {
        builder.ToTable("Roles");
        
        builder.HasKey(x => x.Id).HasName("PK_Roles");
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.Name).HasMaxLength(50);
        
        builder.HasMany(r => r.Users)
            .WithOne(p => p.Roles);
        
        builder.Property(x => x.Name).HasDefaultValue(Constentes.RoleCl);
    }
}