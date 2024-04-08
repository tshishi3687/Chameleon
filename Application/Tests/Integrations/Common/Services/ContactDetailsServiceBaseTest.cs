using System.Runtime;
using Chameleon.Application.Common.Business.Services;
using Xunit;

namespace Chameleon.Application.Tests.Integrations.Common.Services;

public class ContactDetailsServiceBaseTest : BaseModelsForTests
{
    [Fact]
    public void CrudServiceTest()
    {
        var contactDetailsService = new ContactDetailsServiceBase(CreateDbContext());

        // CreateEntity
        Assert.Throws<AmbiguousImplementationException>(() => contactDetailsService.CreateEntity(AddBadContactDetailsDto()));
        
        var createContactDetailsDto = contactDetailsService.CreateEntity(AddContactDetailsDto());
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
}