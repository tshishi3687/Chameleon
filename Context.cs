using System.Data.Common;
using Chameleon.DataAccess.Entity;
using Chameleon.DataAccess.EntityConfig;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Chameleon;

public class Context : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<UsersRoles> UsersRoles { get; set; }

    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            "server=localhost;user id=root;password=tshishi;database=Chameleons",
            new MariaDbServerVersion(new Version(10, 5, 4)),
            mySqlOptions => {}
        );
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