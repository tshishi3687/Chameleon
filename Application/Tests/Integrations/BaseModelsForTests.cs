using System.Globalization;
using System.Security.Claims;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.HumanSetting;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Chameleon.Application.Securities;

namespace Chameleon.Application.Tests.Integrations;

public abstract class BaseModelsForTests: BaseContextForTests
{

    private readonly Constantes _iContent = new(CreateDbContext());
    protected static CreationUserDto NoMatchPassword()
    {
        return new CreationUserDto
        {
            PassWord = "12345678",
            PassWordCheck = "678910"
        };
    }
    protected static CreationUserDto IsNotAdult()
    {
        return new CreationUserDto
        {
            PassWord = "12345678",
            PassWordCheck = "12345678",
            BursDateTime = DateTime.Now
        };
    }
    
    protected static CreationUserDto UpdateCreationUserDtoChangePasswordAndLocalityNAme()
    {
        return new CreationUserDto
        {
            FirstName = "Tshishi",
            LastName = "Ced",
            BursDateTime = DateTime.ParseExact("18-10-1987", "dd-MM-yyyy", CultureInfo.InvariantCulture),
            Email = "Jean-Claude.Van_Damme@JCVD.com",
            Phone = "078555557",
            PassWord = "JCVD.12345678",
            PassWordCheck = "JCVD.12345678",
            ContactDetails = new()
            {
                UpdateContactDetailsDto()
            },
            Roles = [AddRoles()]
        };
    }
    
    protected static CreationUserDto NewCreationUserDto()
    {
        return new CreationUserDto
        {
            FirstName = "Tshishi",
            LastName = "Ced",
            BursDateTime = DateTime.ParseExact("18-10-1987", "dd-MM-yyyy", CultureInfo.InvariantCulture),
            Email = "Jean-Claude.VanDamme@JCVD1.com",
            Phone = "07855555877",
            PassWord = "JCVD.12345678",
            PassWordCheck = "JCVD.12345678",
            ContactDetails = new()
            {
                UpdateContactDetailsDto()
            },
            Roles = [AddRoles()]
        };
    }

    private static EnumUsersRoles AddRoles()
    {
        return EnumUsersRoles.ADMIN;
    }
    
    protected static CreationUserDto AddCreationUserDto()
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
                AddContactDetailsDto()
            }
        };
    }

    protected static LocalityDto AddLocalityDto()
    {
        return new LocalityDto { Name = "1000 Bruxelles" };
    }

    protected static LocalityDto UpdateLocalityDto()
    {
        return new LocalityDto { Name = "5000 Namur" };
    }

    protected static CountryDto AddCountryDto()
    {
        return new CountryDto { Name = "Belgique" };
    }

    protected static CountryDto UpdateCountryDto()
    {
        return new CountryDto { Name = "France" };
    }
    
    

    protected static ContactDetailsDto AddContactDetailsDto()
    {
        return new ContactDetailsDto
        {
            Address = "Peace street",
            Number = "44",
            Locality = AddLocalityDto(),
            Country = AddCountryDto()
        };
    }

    protected static ContactDetailsDto UpdateContactDetailsDto()
    {
        return new ContactDetailsDto
        {
            Address = "Peace street",
            Number = "10",
            Locality = UpdateLocalityDto(),
            Country = UpdateCountryDto()
        };
    }

    protected static ContactDetailsDto AddBadContactDetailsDto()
    {
        return new ContactDetailsDto
        {
            Address = "",
            Number = "",
            Locality = AddLocalityDto(),
            Country = AddCountryDto()
        };
    }

    protected static CreationCompanyAndUserDto AddBadCreationCompanyAndUserDtoName()
    {
        return new CreationCompanyAndUserDto
        {
            Name = "",
            BusinessNumber = ""
        };
    }

    protected static CreationCompanyAndUserDto AddBadCreationCompanyAndUserDtoBusinessNumber()
    {
        return new CreationCompanyAndUserDto
        {
            Name = "test",
            BusinessNumber = ""
        };
    }

    protected static CreationCompanyAndUserDto AddBadCreationCompanyAndUserDtoUserIdAndTutor()
    {
        return new CreationCompanyAndUserDto
        {
            Name = "Name",
            BusinessNumber = "Business Number"
            // User null
            // tutor null
        };
    }

    protected static CreationCompanyAndUserDto AddValidCreationCompanyAndUserDto()
    {
        return new CreationCompanyAndUserDto
        {
            Name = "Name",
            BusinessNumber = "Business Number",
            AddCompanyUser = new AddCompanyUser
            {
                CreationUserDto = AddCreationUserDto()
            },
            ContactDetail = AddContactDetailsDto()
        };
    }

    protected static AddCompanyUser AddCompanyUser(Guid guid)
    {
        return new AddCompanyUser
        {
            UserId = guid,
            CreationUserDto = UpdateCreationUserDtoChangePasswordAndLocalityNAme()
        };
    }

    protected static LocalityDto AddBadLocalityEmptyNAme()
    {
        return new LocalityDto();
    }

    protected static LocalityDto AddBadLocalityNameLength0()
    {
        return new LocalityDto()
        {
            Name = ""
        };
    }

    protected static LocalityDto AddBadLocalityNameWithSpace()
    {
        return new LocalityDto
        {
            Name = " "
        };
    }

    protected static CountryDto AddBadCountryEmptyNAme()
    {
        return new CountryDto();
    }

    protected static CountryDto AddBadCountryNameLength0()
    {
        return new CountryDto()
        {
            Name = ""
        };
    }

    protected static CountryDto AddBadCountryNameWithSpace()
    {
        return new CountryDto
        {
            Name = " "
        };
    }
}