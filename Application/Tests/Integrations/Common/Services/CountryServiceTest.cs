using System.Runtime;
using Chameleon.Application.Common.Business.Services;
using Xunit;

namespace Chameleon.Application.Tests.Integrations.Common.Services;

public class CountryServiceTest: BaseModelsForTests
{
    private readonly Context _context;

    public CountryServiceTest()
    {
        _context = new MockContext();
    }
    
    [Fact]
    public void CrudServiceTest()
    {
        var service = new CountryService(_context);

        Assert.Throws<AmbiguousImplementationException>(() =>
            service.AddOrCreateCountry(AddBadCountryEmptyNAme()));
        Assert.Throws<AmbiguousImplementationException>(() =>
            service.AddOrCreateCountry(AddBadCountryNameLength0()));
        Assert.Throws<AmbiguousImplementationException>(() =>
            service.AddOrCreateCountry(AddBadCountryNameWithSpace()));

        var country = service.AddOrCreateCountry(AddCountryDto());
        Assert.NotNull(country);
        Assert.Equal(country.Name, AddCountryDto().Name);
        
    }
}