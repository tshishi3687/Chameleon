using Microsoft.EntityFrameworkCore;

namespace Chameleon.Tests.Integrations;

public abstract class BaseTestContext<TContext> where TContext : DbContext
{
    protected TContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TContext>();
        ConfigureForTesting(optionsBuilder);
        return (TContext)Activator.CreateInstance(typeof(TContext), optionsBuilder.Options);
    }

    private void ConfigureForTesting(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TestDatabase");
    }
}