using Chameleon.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.DataAccess.EntityConfig;

public class UserConfig: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // nom de la table
        builder.ToTable("User");

        // Primary Key
        builder.HasKey(x => x.Id).HasName("PK_user");
        //Rendre la Primary Key auto-incrémantable
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        // gestion des null
        builder.Property(x => x.FirstName).IsRequired(); 
        builder.Property(x => x.LastName).IsRequired(); 
        builder.Property(x => x.BursDateTime).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.EmailEncoding).IsRequired();
        builder.Property(x => x.Phone).IsRequired();
        builder.Property(x => x.PhoneEncoding).IsRequired();
        builder.Property(x => x.PassWord).IsRequired();

        // Prés recquis
        builder.Property(x => x.FirstName).HasMaxLength(50);
        builder.Property(x => x.FirstName).HasMaxLength(50); 
        builder.HasIndex(x => x.Email).IsUnique();
    }
}