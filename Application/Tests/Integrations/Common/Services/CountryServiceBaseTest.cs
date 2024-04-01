using System.Runtime;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Xunit;

namespace Chameleon.Application.Tests.Integrations.Common.Services;

public class CountryServiceBaseTest : BaseModelsForTests
{
    [Fact]
    public void CrudServiceTest()
    {
        var countryService = new HumanSetting.Business.Services.CountryServiceBase(CreateDbContext());

        var badDto1 = new CountryDto();
        Assert.Throws<AmbiguousImplementationException>(() => countryService.CreateEntity1(badDto1));

        var badDto2 = new CountryDto { Name = "" };
        Assert.Throws<AmbiguousImplementationException>(() => countryService.CreateEntity1(badDto2));

        var badDto3 = new CountryDto { Name = " " };
        Assert.Throws<AmbiguousImplementationException>(() => countryService.CreateEntity1(badDto3));

        var countryDto = AddCountryDto();

        // CreateEntity
        var createdCountryDto = countryService.CreateEntity1(countryDto);
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
        var updateDto = UpdateCountryDto();
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