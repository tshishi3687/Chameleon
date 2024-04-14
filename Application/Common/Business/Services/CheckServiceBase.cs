using System.Runtime;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Dtos;

namespace Chameleon.Application.Common.Business.Services;

public abstract class CheckServiceBase(Context context)
{
    protected readonly Context Context = context ?? throw new ArgumentNullException(nameof(context));

    protected static void PasswordMatch(CreationUserDto dto)
    {
        if (!dto.PassWord.Equals(dto.PassWordCheck))
        {
            throw new ArgumentException("Passwords do not match.");
        }
    }

    protected void IsAdult(DateTime dateTime)
    {
        var isAdult = (DateTime.Now.Year - dateTime.Year) >= 18;
        if (!isAdult)
        {
            throw new ArgumentException("You are not an adult");
        }
    }

    protected void UniqueUser(CreationUserDto dto)
    {
        if (Context.User.Any(u => u.Email.Equals(dto.Email) || u.Phone.Equals(dto.Phone)))
        {
            throw new Exception($"There already exists a user with this email: {dto.Email} or this phone {dto.Phone}!");
        }
    }

    protected static void CheckAddressAndNumber(ContactDetailsDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Address))
        {
            throw new AmbiguousImplementationException("Dto address's can't be null!");
        }

        if (string.IsNullOrWhiteSpace(dto.Number))
        {
            throw new AmbiguousImplementationException("Dto address's can't be null!");
        }
    }

    protected static void CheckCreationCompanyAndUser(CreationCompanyAndUserDto dto)
    {
        if (dto == null || (dto.AddCompanyUser != null && dto.AddCompanyUser.UserId == null &&
                            dto.AddCompanyUser.CreationUserDto == null))
        {
            throw new ArgumentException(
                "The User Id or the AddCompanyUser object cannot both be empty at the same time. One or the other!");
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new ArgumentException("Company name can't be null!");
        }

        if (string.IsNullOrWhiteSpace(dto.BusinessNumber))
        {
            throw new ArgumentException("Company business number can't be null!");
        }
    }
}