using Chameleon.Application.HumanSetting.Business.Services;
using Chameleon.Application.Securities;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace Chameleon.Application.Tests.Integrations.HumanSettingTest.Services;

public class UserServiceTest : BaseModelsForTests
{
    [Fact]
    public void CrudService()
    {
        var context = CreateDbContext();
        var service = new UserService(context);

        // CreateEntity (exception) 
        Assert.Throws<ArgumentException>(() => service.CreateEntity(NoMatchPassword()));
        Assert.Throws<ArgumentException>(() => service.CreateEntity(IsNotAdult()));

        var user = service.CreateEntity(AddCreationUserDto());
        Assert.NotNull(user);
        Assert.Throws<Exception>(() => service.CreateEntity(AddCreationUserDto()));
        Assert.Equal(user.FirstName, AddCreationUserDto().FirstName);
        Assert.Equal(user.LastName, AddCreationUserDto().LastName);
        Assert.Equal(user.BursDateTime, AddCreationUserDto().BursDateTime);
        Assert.Equal(user.Email, AddCreationUserDto().Email);
        Assert.Equal(user.Phone, AddCreationUserDto().Phone);
        Assert.True(new MdpCrypte().Compart(user.PassWord, AddCreationUserDto().PassWord));
        Assert.False(user.UserContactDetails().IsNullOrEmpty());
        Assert.True(user.UserContactDetails().Count == 1);
        Assert.Equal(user.UserContactDetails().First().ContactDetails.Address, AddCreationUserDto().ContactDetails.First().Address);
        Assert.Equal(user.UserContactDetails().First().ContactDetails.Number, AddCreationUserDto().ContactDetails.First().Number);
        Assert.Equal(user.UserContactDetails().First().ContactDetails.Locality.Name, AddCreationUserDto().ContactDetails.First().Locality.Name);
        Assert.Equal(user.UserContactDetails().First().ContactDetails.Country.Name, AddCreationUserDto().ContactDetails.First().Country.Name);
        
        //ReadEntity
        var readUser = service.ReadEntity(user.Id);
        Assert.NotNull(readUser);
        Assert.Equal(user, readUser);
        
        // UpdateEntity
        var updateUser =
            service.UpdateEntity(UpdateCreationUserDtoChangePasswordAndLocalityNAme(), readUser.Id);
        Assert.NotNull(updateUser);
        Assert.Throws<KeyNotFoundException>(() => service.ReadEntity(readUser.Id));
    }
}