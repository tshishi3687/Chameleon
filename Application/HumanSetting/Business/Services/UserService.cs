using System.Net;
using System.Security.Claims;
using Chameleon.Application.Common.Business.Services;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Chameleon.Application.Securities;
using Org.BouncyCastle.Security;

namespace Chameleon.Application.HumanSetting.Business.Services;

public class UserService(Context context) : CheckServiceBase(context)
{
    private readonly ContactDetailsService _contactDetailsService = new(context);
    private readonly MdpCrypte _crypto = new();

    public User CreateEntity(CreationUserDto dto)
    {
        if (dto == null) throw new Exception(); //TODO


        PasswordMatch(dto);
        IsAdult(dto.BursDateTime);
        UniqueUser(dto);

        var user = Context.User.Add(new User(Context)
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            BursDateTime = dto.BursDateTime,
            Email = dto.Email,
            Phone = dto.Phone,
            PassWord = _crypto.CryptMdp(dto.PassWord)
        }).Entity;

        var uc = new UsersContactDetails();
        uc.UserId = user.Id;

        foreach (var contactDetailsDto in dto.ContactDetails)
        {
            uc.ContactDetailsId = _contactDetailsService.CreateEntity(contactDetailsDto).Id;
            Context.UsersContactDetails.Add(uc);
        }

        Context.SaveChanges();

        return user;
    }

    public User ReadEntity(Guid guid)
    {
        var user = Context.User.SingleOrDefault(u => u.Id.Equals(guid));
        if (user == null)
        {
            throw new KeyNotFoundException("Unable to find entity with this key");
        }

        return user;
    }

    public User UpdateEntity(CreationUserDto dto, Guid userToModifyGuid)
    {
        var userRemove = Context.User.FirstOrDefault(u => u.Id.Equals(userToModifyGuid));
        if (userRemove == null)
        {
            throw new DllNotFoundException("Entity not found!");
        }

        Context.User.Remove(userRemove);
        return CreateEntity(dto);
    }

    public string Login(LoggerDto dto, Constantes constantes)
    {
        CheckLogger(dto);
        CheckAuthentication(dto);
        var user = Context.User.FirstOrDefault(u =>
            u.Email.Equals(dto.Identification) || u.Phone.Equals(dto.Identification))!;
        var claims = new List<Claim> { new(ClaimTypes.Email, user.Email) };
        var response = new HttpResponseMessage(HttpStatusCode.Accepted);
        response.Headers.Add("user", user.FirstName);
        response.Headers.Add("Authorization", "Bearer " + constantes.GenerateToken(claims));


        return "You are connected!";
    }

    private void CheckAuthentication(LoggerDto dto)
    {
        if (dto == null) throw new ArgumentException("Logger cannot be null!");

        var user = Context.User.SingleOrDefault(u =>
            u.Email.Equals(dto.Identification) || u.Phone.Equals(dto.Identification));
        if (user == null)
        {
            throw new FileNotFoundException("User not found!");
        }

        if (!new MdpCrypte().Compart(user.PassWord, dto.Password!))
        {
            throw new PasswordException("Password no match!");
        }
    }

    private static void CheckLogger(LoggerDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Identification))
        {
            throw new ArgumentException("Identification cannot be null!");
        }

        if (string.IsNullOrWhiteSpace(dto.Password))
        {
            throw new ArgumentException("Password cannot be null!");
        }
    }
}