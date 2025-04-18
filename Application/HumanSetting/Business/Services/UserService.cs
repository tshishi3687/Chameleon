using System.Security.Claims;
using Chameleon.Application.Common.Business.Dtos;
using Chameleon.Application.Common.Business.Services;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Mappers;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Chameleon.Application.Securities;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Security;
using Task = System.Threading.Tasks.Task;

namespace Chameleon.Application.HumanSetting.Business.Services;

public class UserService(Context context) : CheckServiceBase(context)
{
    private readonly ContactDetailsService _contactDetailsService = new(context);
    private readonly MdpCrypte _crypto = new();
    private readonly long _maxFileSizeInBytes = 5 * 1024 * 1024;
    private SimpleUserMapper simpleUserMapper = new();


    private async Task<Users> CreateEntity(CreationUserDto dto)
    {
        if (dto == null) throw new Exception(); //TODO

        PasswordMatch(dto);
        IsAdult(dto.BursDateTime);
        await UniqueUser(dto);

        var user = context.User.Add(new Users(context)
        {
            FirstName = dto.FirstName!,
            LastName = dto.LastName!,
            BursDateTime = dto.BursDateTime,
            Email = dto.Email,
            Phone = dto.Phone,
            PassWord = _crypto.CryptMdp(dto.PassWord)
        }).Entity;

        var uc = new UsersContactDetails
        {
            UserId = user.Id,
            Users = user
        };

        foreach (var cd in dto.ContactDetails!.Select(contactDetailsDto =>
                     _contactDetailsService.CreateEntity(contactDetailsDto)))
        {
            uc.ContactDetailsId = cd.Id;
            uc.ContactDetails = cd;
            context.UsersContactDetails.Add(uc);
        }

        await context.SaveChangesAsync();
        return user;
    }

    public async Task<Users> ReadEntity(Guid guid)
    {
        var user = context.User.SingleOrDefault(u => u.Id.Equals(guid));
        if (user == null)
        {
            throw new KeyNotFoundException(Error.EntityKeyNotFound.ToString());
        }

        return user;
    }


    public async Task CreateUsers(CreationUserDto dto, Guid companyGuid)
    {
        if (dto == null) throw new Exception();

        var company = await context.Companies.FirstOrDefaultAsync(c => c.Id.Equals(companyGuid));
        if (company == null) throw new Exception("Company not found");
        var users = await CreateEntity(dto);

        if (dto.Roles!.Count == 0) throw new Exception("Roles not found");
        dto.Roles.ForEach(role =>
        {
            var roleAdding = context.Roles.Add(new Roles
            {
                Name = role.ToString(),
                Company = company,
                CompanyGuid = company.Id,
            }).Entity;

            context.UsersRoles.Add(new UsersRoles
            {
                UserId = users.Id,
                RoleId = roleAdding.Id,
                Users = users,
                Roles = roleAdding
            });
        });
        context.CompanyUsers.Add(new CompanyUser
        {
            Users = users,
            UserId = users.Id,
            Company = company,
            CompanyId = company.Id
        });

        await context.SaveChangesAsync();
    }

    public async Task<Users> UpdateEntity(CreationUserDto dto, Guid userToModifyGuid)
    {
        var userRemove = await context.User.FirstOrDefaultAsync(u => u.Id.Equals(userToModifyGuid));
        if (userRemove == null)
        {
            throw new DllNotFoundException(Error.EntityNotFound.ToString());
        }

        context.User.Remove(userRemove);
        return await CreateEntity(dto);
    }

    public async Task<Passport> Login(LoggerDto dto)
    {
        CheckLogger(dto);
        var user = await CheckAuthentication(dto);

        return await GenerateClams(user, false, null);
    }

    public async Task<Passport> CreateJwtWithRoles(Users users, Company company)
    {
        return await GenerateClams(users, true, company);
    }

    private void AddCompanyPicture(Users users, Company company, IFormFile file)
    {
        if (file == null) throw new Exception("File is null");
        ProcessProfilePicture(file, company);
    }

    public async Task<Passport> CreateCompanyAndUser(CreationCompanyAndUserDto dto)
    {
        await CheckCompanyDtoAndUserDto(dto);

        var user = await AddUser(dto.UserDto!);
        if (user == null) throw new ArgumentException("NotFound");

        await context.SaveChangesAsync();
        // Add in table Company
        var company = context.Companies.Add(new Company(context)
        {
            Name = dto.Name!,
            BusinessNumber = dto.BusinessNumber!,
            Tutor = user,
            ContentType = "null",
            FileContent = [],
            FileName = "null",
            IsActive = dto.isVisible
        }).Entity;

        // Add in table CompanyUser
        context.CompanyUsers.Add(new CompanyUser
        {
            Company = company,
            CompanyId = company.Id,
            Users = user,
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
            RoleId = role.Id,
        });
        context.UsersRoles.Add(new UsersRoles
        {
            UserId = user.Id,
            Users = user,
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
        await context.SaveChangesAsync();

        return await GenerateClams(user, true, company);
    }

    private async Task<Passport> GenerateClams(Users users, bool isChoseCompany, Company company)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, users.Email),
        };

        var response = new Passport();

        if (isChoseCompany)
        {
            var roles = await context.UsersRoles
                .Where(ur => ur.UserId.Equals(users.Id) && ur.Roles.Company.Id.Equals(company.Id))
                .Select(ur => ur.Roles.Name)
                .ToListAsync();
            response.CompanyName = company.Name;
            response.Reference = company.Id;

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                response.Roles = [];
                response.Roles = roles;
            }
        }

        var token = _crypto.GenerateToken(claims);
        response.Token = token;
        response.UserName = users.LastName[0] + ". " + users.FirstName;

        return response;
    }

    private async Task<Users> CheckAuthentication(LoggerDto dto)
    {
        if (dto == null) throw new ArgumentException(Error.LoggerNull.ToString());

        var user = await context.User.FirstOrDefaultAsync(u =>
            u.Email.Equals(dto.Identification) || u.Phone.Equals(dto.Identification));
        if (user == null)
        {
            throw new FileNotFoundException(Error.NotFound.ToString());
        }

        if (_crypto.Compart(user.PassWord, dto.Password!)) return user;

        throw new PasswordException(Error.NotFound.ToString());
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

    public async Task<ICollection<SimpleUserDto>> GetCompanyUser(Guid companyId)
    {
        return await context.CompanyUsers
            .Where(cu => cu.CompanyId == companyId)
            .Select(cu => new SimpleUserDto
            {
                Id = cu.Users.Id,
                FirstName = cu.Users.FirstName,
                LastName = cu.Users.LastName,
                Email = cu.Users.Email,
                Phone = cu.Users.Phone,
                Roles = context.UsersRoles
                    .Where(ur => ur.UserId == cu.UserId)
                    .Join(context.Roles,
                        ur => ur.RoleId,
                        r => r.Id,
                        (ur, r) => r)
                    .Where(r => r.CompanyGuid == companyId || r.CompanyGuid == Guid.Empty)
                    .Select(r => r.Name)
                    .ToList()
            })
            .ToListAsync();

    }

// public bool isKnown(Company company, string identification)
// {
//     var companyUser = company.CompanyUser();
//     var users = new List<User?>();
//     foreach (var user in companyUser)
//     {
//         users.Add(context.User.FirstOrDefault(u => u.Id.Equals(user.UserId)));
//     }
//
//     return users.Any(u => u.Email.Equals(identification) || u.Phone.Equals(identification));
// }

    public async Task<CompanyPictureDto> GetCompanyPictureDto(Guid companyId, Users users)
    {
        var company = context.Companies.FirstOrDefault(c => c.Id == companyId);
        if (company == null) throw new FileNotFoundException(Error.NotFound.ToString());

        return new CompanyPictureDto
        {
            FileContent = company.FileContent,
            FileName = company.FileName,
            ContentType = company.ContentType,
        };
    }


    private async Task<Users> AddUser(CreationUserDto dto)
    {
        var user = await context.User.FirstOrDefaultAsync(u => u.Email.Equals(dto.Email));
        if (user != null) return user;
        return await CreateEntity(dto);
    }

    private void ProcessProfilePicture(IFormFile? file, Company company)
    {
        ArgumentNullException.ThrowIfNull(file);

        if (file.Length > _maxFileSizeInBytes)
        {
            throw new Exception("Ce fichier est trop lourd. Poid Max authorisé est de 5MB");
        }

        var allowedContentTypes = new List<string> { "image/jpeg", "image/png", "image/gif" };
        if (!allowedContentTypes.Contains(file.ContentType))
        {
            throw new Exception("Ce type de fichier n'est pas authorisé.");
        }

        byte[] fileContent;
        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            fileContent = memoryStream.ToArray();
        }


        company.FileName = file.FileName;
        company.ContentType = file.ContentType;
        company.FileContent = fileContent;
        context.Companies.Update(company);
        context.SaveChanges();
    }
}