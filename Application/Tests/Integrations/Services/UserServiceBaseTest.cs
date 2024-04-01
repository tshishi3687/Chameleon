using System.Globalization;
using Chameleon.Business.Dto;
using Chameleon.Business.Dtos;
using Chameleon.Business.Services;
using Xunit;

namespace Chameleon.Tests.Integrations.Services;

public class UserServiceBaseTest : BaseTestContext
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
        Assert.Equal(readUserDto.ContactDetails.FirstOrDefault().Address, creationUserDto.ContactDetails.FirstOrDefault().Address);
        
        // UpdateEntoty
        var updatDto =
            creationUserService.UpdateEntity(UpdateCreationUserDtoChangePasswordAndLocalityNAme(), readUserDto.Id);
        Assert.NotNull(updatDto);
        Assert.Throws<KeyNotFoundException>(() => userVueService.ReadEntity(readUserDto.Id));
        Assert.Equal(UpdateCreationUserDtoChangePasswordAndLocalityNAme().ContactDetails.FirstOrDefault().Locality.Name, updatDto.ContactDetails.FirstOrDefault().Locality.Name);

    }

    private static CreationUserDto UpdateCreationUserDtoChangePasswordAndLocalityNAme()
    {
        return new CreationUserDto
        {
            FirstName = "Jean-Claude",
            LastName = "Van Damme",
            BursDateTime = DateTime.ParseExact("18-10-1987", "dd-MM-yyyy", CultureInfo.InvariantCulture),
            Email = "Jean-Claude.Van-Damme@JCVD.com",
            Phone = "078555555",
            PassWord = "JCVD.12345678",
            PassWordCheck = "JCVD.12345678",
            ContactDetails = new()
            {
                new()
                {
                    Address = "Peace street",
                    Number = "44",
                    Locality = new LocalityDto
                    {
                        Name = "RTRF 55444"
                    },
                    Country = new CountryDto
                    {
                        Name = "Belgique"
                    }
                }
            }
        };
    }

    private static CreationUserDto AddCreationUserDto()
    {
        return new CreationUserDto
        {
            FirstName = "Jean-Claude",
            LastName = "Van Damme",
            BursDateTime = DateTime.ParseExact("18-10-1987", "dd-MM-yyyy", CultureInfo.InvariantCulture),
            Email = "Jean-Claude.Van-Damme@JCVD.com",
            Phone = "078555555",
            PassWord = "12345678",
            PassWordCheck = "12345678",
            ContactDetails = new()
            {
                new()
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
                }
            }
        };
    }

    private static CreationUserDto NoMatchPassword()
    {
        return new CreationUserDto
        {
            PassWord = "12345678",
            PassWordCheck = "678910"
        };
    }
}