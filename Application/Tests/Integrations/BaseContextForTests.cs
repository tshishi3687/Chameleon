using Microsoft.EntityFrameworkCore;

namespace Chameleon.Application.Tests.Integrations;

public abstract class BaseContextForTests : DbContext
{
    protected static Context CreateDbContext()
    {
        return new MockContext();
    }
}


