using Chameleon.Business.Dtos;
using Chameleon.Business.Services;
using Xunit;

namespace Chameleon.Tests.Integrations.Services;

public class LocalityServiceTest: BaseTestContext<Context>
{
    [Fact]
    public void CreateEntityTest()
    {
        using var context = CreateDbContext(); 
        var localityService = new LocalityService(context); 

        var localityDto = new LocalityDto
        {
            Name = "test"
        };

        var createdLocalityDto = localityService.CreateEntity(localityDto);

        Assert.NotNull(createdLocalityDto);
    }
}