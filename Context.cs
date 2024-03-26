using Chameleon.DataAccess.Entity;
using Chameleon.DataAccess.EntityConfig;
using Microsoft.EntityFrameworkCore;

namespace Chameleon;

public class Context : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<UsersRoles> UsersRoles { get; set; }
    
    public DbSet<Locality> Localities { get; set; }
    
    public DbSet<Country> Countries { get; set; }
    
    public DbSet<ContactDetails> ContactDetails { get; set; }
    public DbSet<UsersContactDetails> UsersContactDetails { get; set; }

    
    
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
        modelBuilder.ApplyConfiguration<Locality>(new LocalityConfig());
        modelBuilder.ApplyConfiguration<Country>(new CountryConfig());
        modelBuilder.ApplyConfiguration<ContactDetails>(new ContactDetailsConfig());
        modelBuilder.ApplyConfiguration<UsersContactDetails>(new UserContactDetailsConfig());

        // Allimenter la DB


        //DonneesDB.Pays(pays);


        // Add data
        base.OnModelCreating(modelBuilder);
    }
}