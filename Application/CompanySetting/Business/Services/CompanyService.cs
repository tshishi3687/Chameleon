using Chameleon.Application.Common.Business.Services;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Chameleon.Application.CompanySetting.Business.Services;

public class CompanyService(Context context) : CheckServiceBase(context)
{
    private readonly UserService _userService = new(context);


    public Company AddUserInCompany(CreationUserDto dto, Company company)
    {
        CheckUserDto(dto);
        if (dto.Roles.IsNullOrEmpty()) throw new Exception("Roles");
        var companyUser = AddCompanyUser(company, AddUser(dto));

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

    private void AddUserRoles(User user, CreationUserDto dto, Company company)
    {
        foreach (var role in dto.Roles)
        {
            if (role.Equals(EnumUsersRoles.SUPER_ADMIN))
            {
                throw new Exception($"This roles with number 0, not exist!!");
            }

            var r = context.Roles.Add(
                new Roles
                {
                    Name = role.ToString(),
                    Company = company
                }).Entity;
            context.UsersRoles.Add(new UsersRoles
            {
                UserId = user.Id,
                User = user,
                RoleId = r.Id,
                Roles = r
            });
        }
    }

    private User AddUser(CreationUserDto dto)
    {
        var user = context.User.FirstOrDefault(u => u.Email.Equals(dto.Email));
        return user ?? _userService.CreateEntity(dto);
    }

    private CompanyUser AddCompanyUser(Company companyConcerned, User user)
    {
        return context.CompanyUsers.Add(new CompanyUser
        {
            CompanyId = companyConcerned.Id,
            Company = companyConcerned,
            User = user,
            UserId = user.Id,
            IsActive = true
        }).Entity;
    }
}