using Chameleon.Application.HumanSetting.Business.Services;
using Chameleon.Application.Securities;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace Chameleon.Application.Tests.Integrations.HumanSettingTest.Services;

public class UserServiceTest : BaseModelsForTests
{
    private readonly Context _context;

    public UserServiceTest()
    {
        _context = new MockContext();
    }

    [Fact]
    public void CrudService()
    {
        var service = new UserService(_context);

        // CreateEntity (exception) 
        Assert.Throws<ArgumentException>(() => service.CreateEntity(NoMatchPassword()));
        Assert.Throws<ArgumentException>(() => service.CreateEntity(IsNotAdult()));

        var userDto = AddCreationUserDto();
        var user = service.CreateEntity(userDto);
        Assert.NotNull(user);
        Assert.Throws<Exception>(() => service.CreateEntity(userDto));
        Assert.Equal(user.FirstName, userDto.FirstName);
        Assert.Equal(user.LastName, userDto.LastName);
        Assert.Equal(user.BursDateTime, userDto.BursDateTime);
        Assert.Equal(user.Email, userDto.Email);
        Assert.Equal(user.Phone, userDto.Phone);
        Assert.True(new MdpCrypte().Compart(user.PassWord, userDto.PassWord));
        Assert.False(user.UserContactDetails().IsNullOrEmpty());
        Assert.True(user.UserContactDetails().Count == 1);
        Assert.Equal(user.UserContactDetails().First().ContactDetails.Address, userDto.ContactDetails.First().Address);
        Assert.Equal(user.UserContactDetails().First().ContactDetails.Number, userDto.ContactDetails.First().Number);
        Assert.Equal(user.UserContactDetails().First().ContactDetails.Locality.Name, userDto.ContactDetails.First().Locality.Name);
        Assert.Equal(user.UserContactDetails().First().ContactDetails.Country.Name, userDto.ContactDetails.First().Country.Name);
        
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