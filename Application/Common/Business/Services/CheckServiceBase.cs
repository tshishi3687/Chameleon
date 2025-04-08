using System.Runtime;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Chameleon.Application.Common.Business.Services;

public abstract class CheckServiceBase(Context context)
{
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

    protected async Task UniqueUser(CreationUserDto dto)
    {
        if ( await context.User.AnyAsync(u => u.Email.Equals(dto.Email) || u.Phone.Equals(dto.Phone)))
        {
            throw new Exception($"There already exists a user with this email: {dto.Email} or this phone {dto.Phone}!");
        }
    }

    protected async Task UniqueUserAsync(CreationUserDto dto)
    {
        if (await context.User.AnyAsync(u => u.Email.Equals(dto.Email) || u.Phone.Equals(dto.Phone)))
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

    protected async Task  CheckCompanyDtoAndUserDto(CreationCompanyAndUserDto dto)
    {
        if (dto == null) throw new Exception("Dto can't be null!!");
        await CheckCompany(dto);
        if (dto.UserDto == null) throw new Exception("User dto can't be null");
        CheckUserDto(dto.UserDto);
    }

    private async Task CheckCompany(CreationCompanyAndUserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name)) throw new ArgumentException("Company name can't be null!");
        if (string.IsNullOrWhiteSpace(dto.BusinessNumber)) throw new ArgumentException("Company business number can't be null!");
        var check = await context.Companies.AnyAsync(c => c.BusinessNumber.Equals(dto.BusinessNumber));
        if (check ) throw new Exception("There already exists a company with this business number");
    }

    protected static void CheckUserDto(CreationUserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName)) throw new ArgumentException("User firstName can't be null!");
        if (string.IsNullOrWhiteSpace(dto.LastName)) throw new ArgumentException("User lastName can't be null!");
        if (string.IsNullOrWhiteSpace(dto.Email)) throw new ArgumentException("User email can't be null!");
        if (string.IsNullOrWhiteSpace(dto.Phone)) throw new ArgumentException("User phone can't be null!");
        if (string.IsNullOrWhiteSpace(dto.PassWord)) throw new ArgumentException("User password can't be null!");
        if (!dto.PassWord.Equals(dto.PassWordCheck)) throw new ArgumentException("Password and CheckPassword no match!");
        
    }
}