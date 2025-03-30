using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Chameleon.Application.Common.Business.Services;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
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

    public Data Login(LoggerDto dto, Constantes constantes)
    {
        CheckLogger(dto);
        var user = CheckAuthentication(dto);

        return GenerateClams(user, constantes, false, null);
    }

    public Data CreateJwtWithRoles(Constantes constantes, Guid companyGuid)
    {
        return GenerateClams(constantes.Connected, constantes, true, companyGuid);
    }

    public Data CreateCompanyAndUser(CreationCompanyAndUserDto dto, Constantes constantes)
    {
        CheckCompanyDtoAndUserDto(dto);

        var user = AddUser(dto.UserDto!);
        if (user == null) throw new ArgumentException("NotFound");

        // Add in table Company
        var company = context.Companies.Add(new Company(context)
        {
            Name = dto.Name!,
            BusinessNumber = dto.BusinessNumber!,
            Tutor = user
        }).Entity;

        // Add in table CompanyUser
        context.CompanyUsers.Add(new CompanyUser
        {
            Company = company,
            CompanyId = company.Id,
            User = user,
            UserId = user.Id,
            IsActive = true
        });

        var role = context.Roles.Add(new Roles
        {
            Name = EnumUsersRoles.SUPER_ADMIN.ToString(),
            Company = company,
            Users = new List<UsersRoles>()
        }).Entity;

        // Add in table UserRoles
        context.UsersRoles.Add(new UsersRoles
        {
            UserId = user.Id,
            User = user,
            RoleId = role.Id,
            Roles = role
        });
        context.UsersRoles.Add(new UsersRoles
        {
            UserId = user.Id,
            User = user,
            RoleId = context.Roles.Add(new Roles
            {
                Name = EnumUsersRoles.ADMIN.ToString(),
                Company = company,
                Users = new List<UsersRoles>()
            }).Entity.Id
        });
        context.UsersRoles.Add(new UsersRoles
        {
            UserId = user.Id,
            RoleId = context.Roles.Add(new Roles
            {
                Name = EnumUsersRoles.CUSTOMER.ToString(),
                Company = company,
                Users = new List<UsersRoles>()
            }).Entity.Id
        });
        context.UsersRoles.Add(new UsersRoles
        {
            UserId = user.Id,
            RoleId = context.Roles.Add(new Roles
            {
                Name = EnumUsersRoles.WORKER.ToString(),
                Company = company,
                Users = new List<UsersRoles>()
            }).Entity.Id
        });

        // Save All insert.
        context.SaveChanges();

        return GenerateClams(user, constantes, false, null);
    }

    private Data GenerateClams(User user, Constantes constantes, bool isChoseCompany, Guid? companyGuid)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
        };

        var response = new Data();

        var companyName = context.Companies.FirstOrDefault(c => c.Id.Equals(companyGuid))?.Name;
        if (isChoseCompany)
        {
            var roles = context.UsersRoles
                .Where(ur => ur.UserId.Equals(user.Id) && ur.Roles.Company.Id.Equals(companyGuid))
                .Select(ur => ur.Roles.Name)
                .ToList();
            response.CompanyName = companyName!;
            response.Reference = companyGuid!.Value!;

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                response.Roles = [];
                response.Roles = roles;
            }
        }

        var token = constantes.GenerateToken(claims);
        response.Token = "Bearer " + token;
        response.UserName = user.LastName[0] + ". " + user.FirstName;

        return response;
    }


    private User CheckAuthentication(LoggerDto dto)
    {
        if (dto == null) throw new ArgumentException(Error.LoggerNull.ToString());

        var user = context.User.SingleOrDefault(u =>
            u.Email.Equals(dto.Identification) || u.Phone.Equals(dto.Identification));
        if (user == null)
        {
            throw new FileNotFoundException(Error.NotFound.ToString());
        }

        if (!_crypto.Compart(user.PassWord, dto.Password!))
        {
            Console.WriteLine(user.PassWord);
            Console.WriteLine(dto.Password);
            throw new PasswordException(Error.NotFound.ToString());
        }
        
        return user;
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

    private User AddUser(CreationUserDto dto)
    {
        var user = context.User.FirstOrDefault(u => u.Email.Equals(dto.Email));
        return user ?? CreateEntity(dto);
    }

}