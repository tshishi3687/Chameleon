using Microsoft.EntityFrameworkCore;

namespace Chameleon;

public class MockContext: Context
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TestDatabase");
    }
}