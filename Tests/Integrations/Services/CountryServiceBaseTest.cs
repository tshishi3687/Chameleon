using System.Runtime;
using Chameleon.Business.Dtos;
using Chameleon.Business.Services;
using Xunit;

namespace Chameleon.Tests.Integrations.Services;

public class CountryServiceBaseTest : BaseTestContext
{
    [Fact]
    public void CrudServiceTest()
    {
        var countryService = new CountryServiceBase(CreateDbContext());

        var badDto1 = new CountryDto();
        Assert.Throws<AmbiguousImplementationException>(() => countryService.CreateEntity(badDto1));

        var badDto2 = new CountryDto { Name = "" };
        Assert.Throws<AmbiguousImplementationException>(() => countryService.CreateEntity(badDto2));

        var badDto3 = new CountryDto { Name = " " };
        Assert.Throws<AmbiguousImplementationException>(() => countryService.CreateEntity(badDto3));

        var countryDto = new CountryDto { Name = "test" };

        // CreateEntity
        var createdCountryDto = countryService.CreateEntity(countryDto);
        Assert.NotNull(createdCountryDto);
        Assert.Equal(createdCountryDto.Name, countryDto.Name.ToUpper());

        // ReadEntity
        var readCountryDto = countryService.ReadEntity(createdCountryDto.Id);
        Assert.NotNull(readCountryDto);
        Assert.Equal(readCountryDto.Name, createdCountryDto.Name);
        Assert.Equal(readCountryDto.Id, createdCountryDto.Id);

        // ReadAllEntity
        var readAllCountryDto = countryService.ReadAllEntity();
        Assert.NotEmpty(readAllCountryDto);
        var dto = readAllCountryDto.First();
        Assert.Equal(dto.Id, readCountryDto.Id);
        Assert.Equal(dto.Name, readCountryDto.Name);

        // UpdateEntity
        var updateDto = new CountryDto { Name = "test 2" };
        var updateEntity = countryService.UpdateEntity(updateDto, readCountryDto.Id);
        Assert.NotEqual(readCountryDto.Name, updateEntity.Name);
        Assert.NotEqual(readCountryDto.Id, updateEntity.Id);
        Assert.Equal(updateEntity.Name, updateDto.Name.ToUpper());
        Assert.Throws<KeyNotFoundException>(() => countryService.ReadEntity(readCountryDto.Id));

        // DeleteEntity
        countryService.DeleteEntity(updateEntity.Id);
        Assert.Throws<KeyNotFoundException>(() => countryService.ReadEntity(updateEntity.Id));
        Assert.Empty(countryService.ReadAllEntity());
    }
}