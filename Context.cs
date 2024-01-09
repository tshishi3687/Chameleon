using System.Data.Common;
using Chameleon.DataAccess.Entity;
using Chameleon.DataAccess.EntityConfig;
using Microsoft.EntityFrameworkCore;

namespace Chameleon;

public class Context : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<UsersRoles> UsersRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;database=Chameleons;user=root;password=tshishi");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Config Entity
        modelBuilder.ApplyConfiguration<Roles>(new RoleConfig());
        modelBuilder.ApplyConfiguration<User>(new UserConfig());
        modelBuilder.ApplyConfiguration<UsersRoles>(new UserRoleConfig());

        // Allimenter la DB


        //DonneesDB.Pays(pays);


        // Add data
        base.OnModelCreating(modelBuilder);
    }
}