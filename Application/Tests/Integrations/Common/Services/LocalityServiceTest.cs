using System.Runtime;
using Chameleon.Application.Common.Business.Services;
using Xunit;

namespace Chameleon.Application.Tests.Integrations.Common.Services;

public class LocalityServiceTest: BaseModelsForTests
{
    
    private readonly Context _context;

    public LocalityServiceTest()
    {
        _context = new MockContext();
    }
    
    [Fact]
    public void CrudServiceTest()
    {
        var service = new LocalityService(_context);

        Assert.Throws<AmbiguousImplementationException>(() =>
            service.AddOrCreateLocality(AddBadLocalityEmptyNAme()));
        Assert.Throws<AmbiguousImplementationException>(() =>
            service.AddOrCreateLocality(AddBadLocalityNameLength0()));
        Assert.Throws<AmbiguousImplementationException>(() =>
            service.AddOrCreateLocality(AddBadLocalityNameWithSpace()));

        var locality = service.AddOrCreateLocality(AddLocalityDto());
        Assert.NotNull(locality);
        Assert.Equal(locality.Name, AddLocalityDto().Name);
    }
}