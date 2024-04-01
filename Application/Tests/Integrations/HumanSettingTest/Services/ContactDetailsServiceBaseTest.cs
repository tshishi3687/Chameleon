using System.Runtime;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Xunit;

namespace Chameleon.Application.Tests.Integrations.HumanSettingTest.Services;

public class ContactDetailsServiceBaseTest : BaseTestContext
{
    [Fact]
    public void CrudServiceTest()
    {
        var contactDetailsService = new ContactDetailsServiceBase(CreateDbContext());

        // CreateEntity
        Assert.Throws<AmbiguousImplementationException>(() => contactDetailsService.CreateEntity1(AddBadContactDetailsDto()));
        
        var createContactDetailsDto = contactDetailsService.CreateEntity1(AddContactDetailsDto());
        Assert.NotNull(createContactDetailsDto);
        Assert.Equal(createContactDetailsDto.Address, AddContactDetailsDto().Address);
        Assert.Equal(createContactDetailsDto.Number, AddContactDetailsDto().Number);
        Assert.NotNull(createContactDetailsDto.Locality); // the rest of the tests are on: LocalityServiceTest.cs
        Assert.NotNull(createContactDetailsDto.Country); // the rest of the tests are on: CountryServiceTest.cs
        
        // ReadEntity
        var readContactDetailsDto = contactDetailsService.ReadEntity(createContactDetailsDto.Id);
        Assert.NotNull(readContactDetailsDto);
        Assert.Equal(readContactDetailsDto.Address, createContactDetailsDto.Address);
        Assert.Equal(readContactDetailsDto.Number, createContactDetailsDto.Number);
        Assert.Equal(readContactDetailsDto.Locality.Id, createContactDetailsDto.Locality.Id);
        Assert.Equal(readContactDetailsDto.Country.Id, createContactDetailsDto.Country.Id);
        
        // ReadAllEntity
        var readAllContactDetails = contactDetailsService.ReadAllEntity();
        Assert.NotEmpty(readAllContactDetails);
        Assert.Single(readAllContactDetails);
        var dto = readAllContactDetails.First();
        Assert.Equal(dto.Address, readContactDetailsDto.Address);
        Assert.Equal(dto.Number, readContactDetailsDto.Number);
        
        // UpdateEntity
        var updateContactDetailsDto = UpdateContactDetailsDto();
        var entityContactDetails = contactDetailsService.UpdateEntity(updateContactDetailsDto, readContactDetailsDto.Id);
        Assert.Equal(readContactDetailsDto.Address, entityContactDetails.Address);
        Assert.NotEqual(readContactDetailsDto.Number, entityContactDetails.Number);
        Assert.NotEqual(readContactDetailsDto.Locality.Name, entityContactDetails.Locality.Name);
        Assert.NotEqual(readContactDetailsDto.Country.Name, entityContactDetails.Country.Name);

        // DeleteEntity
        contactDetailsService.DeleteEntity(entityContactDetails.Id);
        Assert.Throws<KeyNotFoundException>(() => contactDetailsService.ReadEntity(entityContactDetails.Id));
        Assert.Empty(contactDetailsService.ReadAllEntity());
    }

    private static ContactDetailsDto AddContactDetailsDto()
    {
        return new ContactDetailsDto
        {
            Address = "Peace street",
            Number = "44",
            Locality = new LocalityDto
            {
                Name = "1000 Bruxelles"
            },
            Country = new CountryDto
            {
                Name = "Belgique"
            }
        };
    }

    private static ContactDetailsDto UpdateContactDetailsDto()
    {
        return new ContactDetailsDto
        {
            Address = "Peace street",
            Number = "10",
            Locality = new LocalityDto
            {
                Name = "1065 Rome"
            },
            Country = new CountryDto
            {
                Name = "Italie"
            }
        };
    }

    private static ContactDetailsDto AddBadContactDetailsDto()
    {
        return new ContactDetailsDto
        {
            Address = "",
            Number = "",
            Locality = new LocalityDto
            {
                Name = "1000 Bruxelles"
            },
            Country = new CountryDto
            {
                Name = "Belgique"
            }
        };
    }
}