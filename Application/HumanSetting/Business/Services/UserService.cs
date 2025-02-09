using System.Net;
using System.Security.Claims;
using Chameleon.Application.Common.Business.Services;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Mappers;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Chameleon.Application.Securities;
using Org.BouncyCastle.Security;

namespace Chameleon.Application.HumanSetting.Business.Services;

public class UserService(Context context) : CheckServiceBase(context)
{
    private readonly ContactDetailsService _contactDetailsService = new(context);
    private readonly MdpCrypte _crypto = new();
    protected readonly Constantes Constantes;

    public User CreateEntity(CreationUserDto dto)
    {
        if (dto == null) throw new Exception(); //TODO


        PasswordMatch(dto);
        IsAdult(dto.BursDateTime);
        UniqueUser(dto);

        var user = context.User.Add(new User(context)
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            BursDateTime = dto.BursDateTime,
            Email = dto.Email,
            Phone = dto.Phone,
            PassWord = _crypto.CryptMdp(dto.PassWord)
        }).Entity;

        var uc = new UsersContactDetails
        {
            UserId = user.Id,
            User = user
        };

        foreach (var cd in dto.ContactDetails.Select(contactDetailsDto =>
                     _contactDetailsService.CreateEntity(contactDetailsDto)))
        {
            uc.ContactDetailsId = cd.Id;
            uc.ContactDetails = cd;
            context.UsersContactDetails.Add(uc);
        }

        return user;
    }

    public User ReadEntity(Guid guid)
    {
        var user = context.User.SingleOrDefault(u => u.Id.Equals(guid));
        if (user == null)
        {
            throw new KeyNotFoundException(Error.EntityKeyNotFound.ToString());
        }

        return user;
    }

    public User UpdateEntity(CreationUserDto dto, Guid userToModifyGuid)
    {
        var userRemove = context.User.FirstOrDefault(u => u.Id.Equals(userToModifyGuid));
        if (userRemove == null)
        {
            throw new DllNotFoundException(Error.EntityNotFound.ToString());
        }

        context.User.Remove(userRemove);
        return CreateEntity(dto);
    }

    public HttpResponseMessage Login(LoggerDto dto, Constantes constantes)
    {
        CheckLogger(dto);
        CheckAuthentication(dto);
        var user = context.User.FirstOrDefault(u =>
            u.Email.Equals(dto.Identification) || u.Phone.Equals(dto.Identification))!;

        return GenerateClams(user, constantes, false, null);
    }

    public HttpResponseMessage CreateJwtWithRoles(Constantes constantes, Guid companyGuid)
    {
        return GenerateClams(constantes.Connected, constantes, true, companyGuid);
    }

    private HttpResponseMessage GenerateClams(User user, Constantes constantes, bool isChoseCompany, Guid? companyGuid)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
        };

        var response = new HttpResponseMessage(HttpStatusCode.Accepted);

        var companyName = context.Companies.FirstOrDefault(c => c.Id.Equals(companyGuid))?.Name;
        if (isChoseCompany)
        {
            var roles = context.UsersRoles
                .Where(ur => ur.UserId.Equals(user.Id) && ur.Roles.Company.Id.Equals(companyGuid))
                .Select(ur => ur.Roles.Name)
                .ToList();
            response.Headers.Add("CompanyName", companyName);
            response.Headers.Add("Reference", companyGuid.ToString());

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                response.Headers.Add("Roles", role);
            }
        }

        var token = constantes.GenerateToken(claims);
        response.Headers.Add("Authorization", "Bearer " + token);
        response.Headers.Add("User", user.LastName[0] + ". " + user.FirstName);

        return response;
    }


    private void CheckAuthentication(LoggerDto dto)
    {
        if (dto == null) throw new ArgumentException(Error.LoggerNull.ToString());

        var user = context.User.SingleOrDefault(u =>
            u.Email.Equals(dto.Identification) || u.Phone.Equals(dto.Identification));
        if (user == null)
        {
            throw new FileNotFoundException(Error.NotFound.ToString());
        }

        if (!new MdpCrypte().Compart(user.PassWord, dto.Password!))
        {
            throw new PasswordException(Error.NotFound.ToString());
        }
    }

    private static void CheckLogger(LoggerDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Identification))
        {
            throw new ArgumentException(Error.IdentificationRequired.ToString());
        }

        if (string.IsNullOrWhiteSpace(dto.Password))
        {
            throw new ArgumentException(Error.PasswordRequired.ToString());
        }
    }

    public ICollection<User> GetCompanyUser(Company company)
    {
        var cu = company.CompanyUser();
        return cu.Select(companyUser => context.User.FirstOrDefault(u => u.Id.Equals(companyUser.UserId))).ToList();
    }

    public bool isKnown(Company company, string identification)
    {
        var companyUser = company.CompanyUser();
        var users = new List<User?>();
        foreach (var user in companyUser)
        {
            users.Add(context.User.FirstOrDefault(u => u.Id.Equals(user.UserId)));
        }
        return users.Any(u => u.Email.Equals(identification) || u.Phone.Equals(identification));
    }
}