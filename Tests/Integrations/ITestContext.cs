using Microsoft.EntityFrameworkCore;

namespace Chameleon.Tests.Integrations;

public abstract class ITestContext : DbContext
{
    protected Context CreateDbContext()
    {
        var context = new Context();
        context.IsTesting = true;
        return context;
    }
}


