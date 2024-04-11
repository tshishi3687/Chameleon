using System.Runtime;
using Chameleon.Application.Common.Business.Services;
using Xunit;

namespace Chameleon.Application.Tests.Integrations.Common.Services;

public class ContactDetailsServiceTest: BaseModelsForTests
{
    [Fact]
    public void CrudServiceTest()
    {
        var context = CreateDbContext();
        var service = new ContactDetailsService(context);
        
        
        // CreateEntity
        Assert.Throws<AmbiguousImplementationException>(() => service.CreateEntity(AddBadContactDetailsDto()));
        
        var contactDetails = service.CreateEntity(AddContactDetailsDto());
        Assert.NotNull(contactDetails);
        Assert.Equal(contactDetails.Address, AddContactDetailsDto().Address);
        Assert.Equal(contactDetails.Number, AddContactDetailsDto().Number);
        Assert.NotNull(contactDetails.Locality); // the rest of the tests are on: LocalityServiceTest.cs
        Assert.NotNull(contactDetails.Country); // the rest of the tests are on: CountryServiceTest.cs
    }
}