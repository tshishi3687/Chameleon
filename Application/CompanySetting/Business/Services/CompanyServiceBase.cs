using System.Net;
using Chameleon.Application.Common.Business.Services;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Chameleon.Application.Securities;
using Microsoft.IdentityModel.Tokens;

namespace Chameleon.Application.CompanySetting.Business.Services;

public class CompanyServiceBase(Context context) : IContext(context)
{
    private readonly CreationUserServiceBase _creationUserServiceBase = new(context);
    private readonly ContactDetailsServiceBase _contactDetailsServiceBase = new(context);
    private readonly IConstente _constente;

    public HttpResponseMessage CreateEntity(CreationCompanyAndUserDto dto)
    {
        if (CheckIsNullOrWhiteSpace(dto).StatusCode == HttpStatusCode.BadRequest)
        {
            return CheckIsNullOrWhiteSpace(dto);
        }

        var contactDetails = context.ContactDetails.FirstOrDefault(c =>
            c.Id.Equals(_contactDetailsServiceBase.CreateEntity(dto.ContactDetail).Id));
        User user;
        try
        {
            user = AddUser(dto.AddCompanyUser);
        }
        catch (Exception e)
        {
            return new HttpResponseMessage(HttpStatusCode.Conflict)
            {
                Content = new StringContent("This user already exists")
            };
        }

        if (contactDetails == null)
        {
            return new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("Contact Details not found")
            };
        }

        // Add in table Company
        var lastEntity = context.Companies.Add(new Company(context)
        {
            Name = dto.Name,
            BusinessNumber = dto.BusinessNumber,
            ContactDetails = contactDetails,
            Tutor = user
        });

        // Add in table CompanyUser
        context.CompanyUsers.Add(new CompanyUser
        {
            CompanyId = lastEntity.Entity.Id,
            UserId = user.Id
        });

        // Add in table UserRoles
        context.UsersRoles.Add(new UsersRoles
        {
            UserId = user.Id,
            RoleId = context.Roles.Add(new Roles
            {
                Name = EnumUsersRoles.SUPER_ADMIN.ToString(),
                Company = lastEntity.Entity,
                Users = new List<UsersRoles>()
            }).Entity.Id
        });

        // Save All insert.
        context.SaveChanges();

        return new HttpResponseMessage(HttpStatusCode.Created);
    }

    public HttpResponseMessage AddUserInCompany(AddCompanyUser dto, Guid companyGuid)
    {
        if (AllCheck(dto, companyGuid).StatusCode != HttpStatusCode.Accepted)
        {
            return AllCheck(dto, companyGuid);
        }

        var user = AddUser(dto);
        AddUserRoles(user, dto);
        AddCompanyUser(context.Companies.FirstOrDefault(c => c.Id.Equals(companyGuid)), user);

        context.SaveChanges();

        return new HttpResponseMessage(HttpStatusCode.Created);
    }

    private HttpResponseMessage AllCheck(AddCompanyUser dto, Guid companyGuid)
    {
        if (CanDoAction(companyGuid).StatusCode == HttpStatusCode.BadRequest)
        {
            return CanDoAction(companyGuid);
        }

        if (CheckRoles(dto).StatusCode == HttpStatusCode.BadRequest)
        {
            return CheckRoles(dto);
        }

        var companyConcerned = context.Companies.FirstOrDefault(c => c.Id.Equals(companyGuid));
        if (companyConcerned == null)
        {
            return new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("Company not found")
            };
        }

        return new HttpResponseMessage(HttpStatusCode.Accepted);
    }

    private void AddCompanyUser(Company companyConcerned, User user)
    {
        context.CompanyUsers.Add(new CompanyUser
        {
            CompanyId = companyConcerned.Id,
            UserId = user.Id
        });
    }

    private HttpResponseMessage CanDoAction(Guid companyGuid)
    {
        var user = _constente.Connected;
        if (user == null)
        {
            throw new Exception();
        }

        if (!user.UserRoles()
                .Any(ur => ur.Roles.Company.Id.Equals(companyGuid) &&
                           (ur.Roles.Name.Equals(EnumUsersRoles.SUPER_ADMIN) ||
                            ur.Roles.Name.Equals(EnumUsersRoles.ADMIN))))
        {
            return new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent("You are not allowed to do this!")
            };
        }

        return new HttpResponseMessage(HttpStatusCode.Accepted);
    }

    private void AddUserRoles(User user, AddCompanyUser dto)
    {
        foreach (var enumUsersRoles in dto.CreationUserDto.Roles)
        {
            context.UsersRoles.Add(new UsersRoles
            {
                UserId = user.Id,
                RoleId = context.Roles.Add(new Roles
                {
                    Name = enumUsersRoles.ToString()
                }).Entity.Id
            });
        }
    }

    private static HttpResponseMessage CheckRoles(AddCompanyUser dto)
    {
        if (dto.CreationUserDto!.Roles.IsNullOrEmpty())
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Role list cannot be null or empty!")
            };
        }

        if (
            dto.CreationUserDto.Roles.Any(ur => ur.Equals(EnumUsersRoles.SUPER_ADMIN))
        )
        {
            return new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent("You cannot add a user with the Super_admin role")
            };
        }

        return new HttpResponseMessage(HttpStatusCode.Accepted);
    }

    private static HttpResponseMessage CheckIsNullOrWhiteSpace(CreationCompanyAndUserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Dto name's can't be null!")
            };
        }

        if (string.IsNullOrWhiteSpace(dto.BusinessNumber))
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Dto business number's can't be null!")
            };
        }

        return CanGetOrCreateUserOrElseThrow(dto.AddCompanyUser);
    }

    private static HttpResponseMessage CanGetOrCreateUserOrElseThrow(AddCompanyUser dto)
    {
        if (dto == null || (dto.UserId == null && dto.CreationUserDto == null))
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Can't create or found user!")
            };
        }

        return new HttpResponseMessage(HttpStatusCode.Accepted);
    }

    private User TakeUserInContext(Guid guid)
    {
        var user = context.User.FirstOrDefault(u => u.Id.Equals(guid));
        if (user == null)
        {
            throw new FileNotFoundException("User not found!");
        }

        return user;
    }

    [Obsolete("Obsolete")]
    private User TakeUserThroughCreation(CreationUserDto dto)
    {
        var userDto = _creationUserServiceBase.CreateEntity1(dto);
        var userEntity = context.User.FirstOrDefault(u => u.Id.Equals(userDto.Id));
        if (userEntity == null)
        {
            throw new FileNotFoundException("User not found!");
        }

        return userEntity;
    }

    private User AddUser(AddCompanyUser dto)
    {
        return dto switch
        {
            { UserId: not null, CreationUserDto: not null } => TakeUserInContext(dto.UserId.Value),
            { UserId: not null } => TakeUserInContext(dto.UserId.Value),
            _ => TakeUserThroughCreation(dto.CreationUserDto!)
        };
    }
}