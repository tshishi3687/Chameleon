using System.Globalization;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Dtos;

namespace Chameleon.Application.Tests.Integrations;

public abstract class BaseModelsForTests: BaseContextForTests
{
    protected static CreationUserDto NoMatchPassword()
    {
        return new CreationUserDto
        {
            PassWord = "12345678",
            PassWordCheck = "678910"
        };
    }
    
    protected static CreationUserDto UpdateCreationUserDtoChangePasswordAndLocalityNAme()
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
                UpdateContactDetailsDto()
            }
        };
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
            Name = ""
        };
    }

    protected static CreationCompanyAndUserDto AddBadCreationCompanyAndUserDtoBusinessNumber()
    {
        return new CreationCompanyAndUserDto
        {
            Name = "Test",
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
            Tutor = AddCreationUserDto(),
            ContactDetail = AddContactDetailsDto()
        };
    }
}