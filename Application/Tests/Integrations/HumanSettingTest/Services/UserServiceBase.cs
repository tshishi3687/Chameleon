using Chameleon.Application.HumanSetting.Business.Services;
using Xunit;

namespace Chameleon.Application.Tests.Integrations.HumanSettingTest.Services;

public class UserServiceBase : BaseModelsForTests
{
    [Fact]
    public void CrudService()
    {
        var context = CreateDbContext();
        var creationUserService = new CreationUserServiceBase(context);
        var userVueService = new UserVueServiceBase(context);

        // CreateEntity (exception) : password no match
        Assert.Throws<ArgumentException>(() => creationUserService.CreateEntity1(NoMatchPassword()));

        var creationUserDto = creationUserService.CreateEntity1(AddCreationUserDto());
        Assert.NotNull(creationUserDto);
        //CreationEntity (exception) : tests the exception if we add a user with the same data: phone, Email. 
        Assert.Throws<Exception>(() => creationUserService.CreateEntity1(AddCreationUserDto()));

        // ReadEntity
        var readUserDto = userVueService.ReadEntity(creationUserDto.Id);
        Assert.NotNull(readUserDto);
        Assert.Equal(readUserDto.FirstName, creationUserDto.FirstName);
        Assert.Equal(readUserDto.LastName, creationUserDto.LastName);
        Assert.Equal(readUserDto.BursDateTime, creationUserDto.BursDateTime);

        // ReadAllEntity
        Assert.Equal(readUserDto.ContactDetails.Count, 1);
        Assert.Equal(readUserDto.ContactDetails.FirstOrDefault().Address,
            creationUserDto.ContactDetails.FirstOrDefault().Address);

        // UpdateEntoty
        var updatDto =
            creationUserService.UpdateEntity(UpdateCreationUserDtoChangePasswordAndLocalityNAme(), readUserDto.Id);
        Assert.NotNull(updatDto);
        Assert.Throws<KeyNotFoundException>(() => userVueService.ReadEntity(readUserDto.Id));
        Assert.Equal(UpdateCreationUserDtoChangePasswordAndLocalityNAme().ContactDetails.FirstOrDefault().Locality.Name.ToUpper(),
            updatDto.ContactDetails.FirstOrDefault().Locality.Name.ToUpper());
    }
}