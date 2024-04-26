using System.Globalization;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.HumanSetting;
using Chameleon.Application.HumanSetting.Business.Dtos;

namespace Chameleon.Application.Tests.Integrations;

public abstract class BaseModelsForTests
{
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
            Email = "Jean-Claude.Van_Dame@JVD.com",
            Phone = "078555557",
            PassWord = "JVD.12345678",
            PassWordCheck = "JVD.12345678",
            ContactDetails = [UpdateContactDetailsDto()],
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
            Email = "Jean-Claude.VanDame@JVD1.com",
            Phone = "07855555877",
            PassWord = "JVD.12345678",
            PassWordCheck = "JVD.12345678",
            ContactDetails = [UpdateContactDetailsDto()],
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
            LastName = "Van Dame",
            BursDateTime = DateTime.ParseExact("18-10-1987", "dd-MM-yyyy", CultureInfo.InvariantCulture),
            Email = "Jean-Claude.Van-Dame@JVD.com",
            Phone = "078555555",
            PassWord = "12345678",
            PassWordCheck = "12345678",
            ContactDetails = [AddContactDetailsDto()]
        };
    }

    protected static LocalityDto AddLocalityDto()
    {
        return new LocalityDto { Name = "1000 Brulees" };
    }

    private static LocalityDto UpdateLocalityDto()
    {
        return new LocalityDto { Name = "5000 Naur" };
    }

    protected static CountryDto AddCountryDto()
    {
        return new CountryDto { Name = "Belgium" };
    }

    private static CountryDto UpdateCountryDto()
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

    private static ContactDetailsDto UpdateContactDetailsDto()
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
            }
        };
    }

    protected static LocalityDto AddBadLocalityEmptyNAme()
    {
        return new LocalityDto();
    }

    protected static LocalityDto AddBadLocalityNameLength0()
    {
        return new LocalityDto
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
        return new CountryDto
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

    protected CardDto GetBadCardWithAbsentMemory(SimpleUserDto madeBy)
    {
        return new CardDto
        {
            DateTime = DateTime.Now,
            AbsentDetails = GetAbsentDto(madeBy),
            MemoryDetails = GetMemoryDto(madeBy),
            IsEnd = false,
            IsMade = false
        };
    }

    protected CardDto GetBadCardWithEmptyAbsentMemoryAndTaskOrEvent()
    {
        return new CardDto
        {
            DateTime = DateTime.Now,
            IsEnd = false,
            IsMade = false
        };
    }

    protected CardDto GetBadCardWithAbsentTaskOrEvent(SimpleUserDto madeBy, SimpleUserDto simpleUserDto)
    {
        return new CardDto
        {
            DateTime = DateTime.Now,
            AbsentDetails = GetAbsentDto(madeBy),
            TaskOrEventDetails = GetTaskOrEventDto(madeBy, simpleUserDto),
            IsEnd = false,
            IsMade = false
        };
    }

    protected CardDto GetBadCardWithAbsentMemoryAndTaskOrEvent(SimpleUserDto madeBy, SimpleUserDto simpleUserDto)
    {
        return new CardDto
        {
            DateTime = DateTime.Now,
            AbsentDetails = GetAbsentDto(madeBy),
            MemoryDetails = GetMemoryDto(madeBy),
            TaskOrEventDetails = GetTaskOrEventDto(madeBy, simpleUserDto),
            IsEnd = false,
            IsMade = false
        };
    }

    protected CardDto GetValidCardWithTaskOrEvent(SimpleUserDto madeBy, SimpleUserDto simpleUserDto)
    {
        return new CardDto
        {
            DateTime = DateTime.Now,
            TaskOrEventDetails = GetTaskOrEventDto(madeBy, simpleUserDto),
            IsEnd = false,
            IsMade = false
        };
    }

    protected CardDto GetValidCardWithMemory(SimpleUserDto madeBy)
    {
        return new CardDto
        {
            DateTime = DateTime.Now,
            MemoryDetails = GetMemoryDto(madeBy),
            IsEnd = false,
            IsMade = false
        };
    }

    protected CardDto GetValidCardWithAbsent(SimpleUserDto madeBy)
    {
        return new CardDto
        {
            DateTime = DateTime.Now,
            AbsentDetails = GetAbsentDto(madeBy),
            IsEnd = false,
            IsMade = false
        };
    }

    private static TaskOrEventDto GetTaskOrEventDto(SimpleUserDto madeBy, SimpleUserDto simpleUserDto)
    {
        return new TaskOrEventDto
        {
            MadeBy = madeBy,
            MadeById = madeBy.Id,
            Title = "Add controllers: Project EEG",
            Description = "All service's ok, now we need access root for frondEnd",
            Participant = new List<SimpleUserDto>([simpleUserDto])
        };
    }

    private static MemoryDto GetMemoryDto(SimpleUserDto madeBy)
    {
        return new MemoryDto
        {
            MadeBy = madeBy,
            Title = "Meeting with new worker",
            Description = "Cédrick is the best person for me. " +
                          "I think he's the best solution for our company."
        };
    }

    private static AbsentDto GetAbsentDto(SimpleUserDto madeBy)
    {
        return new AbsentDto
        {
            MadeBy =  madeBy
        };
    }
}