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
        var company = Context.Companies.Add(new Company(Context)
        {
            Name = dto.Name,
            BusinessNumber = dto.BusinessNumber,
            ContactDetails = contactDetails,
            Tutor = user
        });

        // Add in table CompanyUser
        Context.CompanyUsers.Add(new CompanyUser
        {
            CompanyId = company.Entity.Id,
            UserId = user.Id
        });

        // Add in table UserRoles
        Context.UsersRoles.Add(new UsersRoles
        {
            UserId = user.Id,
            RoleId = Context.Roles.Add(new Roles
            {
                Name = EnumUsersRoles.SUPER_ADMIN.ToString(),
                Company = company.Entity,
                Users = new List<UsersRoles>()
            }).Entity.Id
        });
        Context.UsersRoles.Add(new UsersRoles
        {
            UserId = user.Id,
            RoleId = Context.Roles.Add(new Roles
            {
                Name = EnumUsersRoles.ADMIN.ToString(),
                Company = company.Entity,
                Users = new List<UsersRoles>()
            }).Entity.Id
        });
        Context.UsersRoles.Add(new UsersRoles
        {
            UserId = user.Id,
            RoleId = Context.Roles.Add(new Roles
            {
                Name = EnumUsersRoles.CUSTOMER.ToString(),
                Company = company.Entity,
                Users = new List<UsersRoles>()
            }).Entity.Id
        });
        Context.UsersRoles.Add(new UsersRoles
        {
            UserId = user.Id,
            RoleId = Context.Roles.Add(new Roles
            {
                Name = EnumUsersRoles.WORKER.ToString(),
                Company = company.Entity,
                Users = new List<UsersRoles>()
            }).Entity.Id
        });
        
        //Add in table Is Active User
        Context.IsActiveUserInCompanies.Add(new IsActiveUserInCompany
        {
            IsActive = true,
            CompanyId = company.Entity.Id,
            UserId = user.Id
        });

        // Save All insert.
        Context.SaveChanges();

        return company.Entity;
    }

    public Company AddUserInCompany(AddCompanyUser dto, Company company)
    {
        var companyUser = AddCompanyUser(company, _userService.CreateEntity(dto.CreationUserDto));
        AddUserRoles(Context.User.FirstOrDefault(u => u.Id.Equals(companyUser.User.Id)), dto, companyUser.Company);

        Context.SaveChanges();
        return companyUser.Company;
    }

    public ICollection<Company> GetMyCompanies(User user)
    {
        var list = new List<Company>();
        foreach (var companyUser in user.Companies())
        {
            list.Add(Context.Companies.First(c => c.Id.Equals(companyUser.CompanyId)));
        }

        return list;
    }
    
    private void AddUserRoles(User user, AddCompanyUser dto, Company company)
    {
        foreach (var enumUsersRoles in dto.CreationUserDto.Roles)
        {
            Context.UsersRoles.Add(new UsersRoles
            {
                UserId = user.Id,
                RoleId = Context.Roles.Add(new Roles
                {
                    Company = company,
                    Name = enumUsersRoles.ToString()
                }).Entity.Id
            });
        }
    }

    private User TakeUserInContext(Guid guid)
    {
        var user = Context.User.FirstOrDefault(u => u.Id.Equals(guid));
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
        return Context.CompanyUsers.Add(new CompanyUser
        {
            CompanyId = companyConcerned.Id,
            UserId = user.Id
        }).Entity;
    }
}