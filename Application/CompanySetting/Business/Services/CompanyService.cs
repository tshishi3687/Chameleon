using Chameleon.Application.Common.Business.Services;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.Business.Services;

public class CompanyService(Context context) : CheckServiceBase(context)
{
    private readonly UserService _userService = new(context);
    private readonly ContactDetailsService _contactDetailsService = new(context);

    public Company CreateCompanyAndUser(CreationCompanyAndUserDto dto)
    {
        if (dto == null) throw new Exception(); //TODO

        CheckCreationCompanyAndUser(dto);

        var contactDetails = _contactDetailsService.CreateEntity(dto.ContactDetail);


        if (contactDetails == null)
        {
            throw new ArgumentException("ContactDetails no create and no found!");
        }

        User user;
        try
        {
            user = AddUser(dto.AddCompanyUser);
        }
        catch (Exception e)
        {
            throw new ArgumentException("User no create and no found");
        }

        // Add in table Company
        var company = context.Companies.Add(new Company(context)
        {
            Name = dto.Name,
            BusinessNumber = dto.BusinessNumber,
            ContactDetails = contactDetails,
            Tutor = user
        });

        // Add in table CompanyUser
        context.CompanyUsers.Add(new CompanyUser
        {
            CompanyId = company.Entity.Id,
            UserId = user.Id
        });

        // Add in table UserRoles
        context.UsersRoles.Add(new UsersRoles
        {
            UserId = user.Id,
            RoleId = context.Roles.Add(new Roles
            {
                Name = EnumUsersRoles.SUPER_ADMIN.ToString(),
                Company = company.Entity,
                Users = new List<UsersRoles>()
            }).Entity.Id
        });

        // Save All insert.
        context.SaveChanges();

        return company.Entity;
    }

    public Company AddUserInCompany(AddCompanyUser dto, Guid companyGuid)
    {
        var companyUser = AddCompanyUser(context.Companies.SingleOrDefault(c => c.Id.Equals(companyGuid)),
            _userService.CreateEntity(dto.CreationUserDto));
        AddUserRoles(context.User.FirstOrDefault(u => u.Id.Equals(companyUser.User.Id)), dto, companyUser.Company);

        context.SaveChanges();
        return companyUser.Company;
    }

    private void AddUserRoles(User user, AddCompanyUser dto, Company company)
    {
        foreach (var enumUsersRoles in dto.CreationUserDto.Roles)
        {
            context.UsersRoles.Add(new UsersRoles
            {
                UserId = user.Id,
                RoleId = context.Roles.Add(new Roles
                {
                    Company = company,
                    Name = enumUsersRoles.ToString()
                }).Entity.Id
            });
        }
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
        var userDto = _userService.CreateEntity(dto);
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

    private CompanyUser AddCompanyUser(Company companyConcerned, User user)
    {
        return context.CompanyUsers.Add(new CompanyUser
        {
            CompanyId = companyConcerned.Id,
            UserId = user.Id
        }).Entity;
    }
}