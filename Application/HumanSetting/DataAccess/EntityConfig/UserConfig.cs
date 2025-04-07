using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.HumanSetting.DataAccess.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id).HasName("PK_user");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.BursDateTime).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.PassWord).IsRequired();

            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();

            builder.Property(x => x.Email)
                .IsRequired()
                .HasAnnotation("RegularExpression", @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,6}$");
            builder.Property(x => x.Phone)
                .IsRequired()
                .HasAnnotation("RegularExpression", @"^(\\+|00)\\d{1,4}[\\s/0-9]*$");
        }
    }
}