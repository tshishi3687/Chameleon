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
            throw new ArgumentException("User no create and no found", e);
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

        var role = context.Roles.Add(new Roles
        {
            Name = EnumUsersRoles.SUPER_ADMIN.ToString(),
            Company = company.Entity,
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
                Company = company.Entity,
                Users = new List<UsersRoles>()
            }).Entity.Id
        });
        context.UsersRoles.Add(new UsersRoles
        {
            UserId = user.Id,
            RoleId = context.Roles.Add(new Roles
            {
                Name = EnumUsersRoles.CUSTOMER.ToString(),
                Company = company.Entity,
                Users = new List<UsersRoles>()
            }).Entity.Id
        });
        context.UsersRoles.Add(new UsersRoles
        {
            UserId = user.Id,
            RoleId = context.Roles.Add(new Roles
            {
                Name = EnumUsersRoles.WORKER.ToString(),
                Company = company.Entity,
                Users = new List<UsersRoles>()
            }).Entity.Id
        });
        
        //Add in table Is Active User
        context.IsActiveUserInCompanies.Add(new IsActiveUserInCompany
        {
            IsActive = true,
            CompanyId = company.Entity.Id,
            UserId = user.Id
        });

        // Save All insert.
        context.SaveChanges();

        return company.Entity;
    }

    public Company AddUserInCompany(AddCompanyUser dto, Company company)
    {
        if (company == null || dto == null) throw new Exception(); //TODO
        var companyUser = AddCompanyUser(company, _userService.CreateEntity(dto.CreationUserDto!));

        AddUserRoles(companyUser.User, dto, companyUser.Company);

        context.SaveChanges();
        return companyUser.Company;
    }

    public ICollection<Company> GetMyCompanies(User user)
    {
        var list = new List<Company>();
        foreach (var companyUser in user.Companies())
        {
            list.Add(context.Companies.First(c => c.Id.Equals(companyUser.CompanyId)));
        }

        return list;
    }
    
    private void AddUserRoles(User user, AddCompanyUser dto, Company company)
    {
        foreach (var enumUsersRoles in dto.CreationUserDto.Roles)
        {
            var role = context.Roles.Add(new Roles
            {
                Company = company,
                Name = enumUsersRoles.ToString()
            }).Entity;
            context.UsersRoles.Add(new UsersRoles
            {
                UserId = user.Id,
                User = user,
                RoleId = role.Id,
                Roles = role
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

    private User TakeUserThroughCreation(CreationUserDto dto)
    {
        return _userService.CreateEntity(dto);
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
            Company = companyConcerned,
            User = user,
            UserId = user.Id
        }).Entity;
    }
}