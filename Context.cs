using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.Common.DataAccess.EntityConfig;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.CompanySetting.DataAccess.EntityConfig;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.DataAccess.EntityConfig;
using Microsoft.EntityFrameworkCore;

namespace Chameleon;

public class Context : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<UsersRoles> UsersRoles { get; set; }
    public DbSet<Locality> Localities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<ContactDetails> ContactDetails { get; set; }
    public DbSet<UsersContactDetails> UsersContactDetails { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyUser> CompanyUsers { get; set; }
    public DbSet<Absent> Absents { get; set; }
    public DbSet<Memory> Memories { get; set; }
    public DbSet<TaskOrEvent> TaskOrEvents { get; set; }
    public DbSet<TaskOrEventUser> TaskOrEventUsers { get; set; }
    public DbSet<Card> Cards { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            "server=localhost;user id=root;password=tshishi;database=Chameleons",
            new MariaDbServerVersion(new Version(10, 5, 4)),
            _ => { }
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Config Entity
        modelBuilder.ApplyConfiguration(new RoleConfig());
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new UserRoleConfig());
        modelBuilder.ApplyConfiguration(new LocalityConfig());
        modelBuilder.ApplyConfiguration(new CountryConfig());
        modelBuilder.ApplyConfiguration(new ContactDetailsConfig());
        modelBuilder.ApplyConfiguration(new UserContactDetailsConfig());
        modelBuilder.ApplyConfiguration(new CompanyConfig());
        modelBuilder.ApplyConfiguration(new CompanyUserConfig());
        modelBuilder.ApplyConfiguration(new AbsentConfig());
        modelBuilder.ApplyConfiguration(new MemoryConfig());
        modelBuilder.ApplyConfiguration(new TaskOrEventConfig());
        modelBuilder.ApplyConfiguration(new TaskOrEventUserConfig());
        modelBuilder.ApplyConfiguration(new CardConfig());

        // Allimenter la DB


        //DonneesDB.Pays(pays);


        // Add data
        base.OnModelCreating(modelBuilder);
    }
}