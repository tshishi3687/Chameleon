using Chameleon.Application.Common.Business.Services;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Mappers;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Chameleon.Application.CompanySetting.Business.Services;

public class CompanyService(Context context) : CheckServiceBase(context)
{
    
    private CompanyEasyVueMapper _companyEasyVueMapper = new();

    // public Company AddUserInCompany(CreationUserDto dto, Company company)
    // {
    //     CheckUserDto(dto);
    //     if (dto.Roles.IsNullOrEmpty()) throw new Exception("Roles");
    //     var companyUser = AddCompanyUser(company, AddUser(dto));
    //
    //     AddUserRoles(companyUser.User, dto, companyUser.Company);
    //
    //     context.SaveChanges();
    //     return companyUser.Company;
    // }

    public async Task<ICollection<CompanyEasyVueDto>> GetMyCompanies(Users users)
    {
        var list = new List<Company>();
        foreach (var companyUser in await users.Companies())
        {
            list.Add(context.Companies.First(c => c.Id.Equals(companyUser.CompanyId)));
        }

        return _companyEasyVueMapper.ToDtos(list);
    }

    private void AddUserRoles(Users users, CreationUserDto dto, Company company)
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
                UserId = users.Id,
                Users = users,
                RoleId = r.Id,
                Roles = r
            });
        }
    }

    // private User AddUser(CreationUserDto dto)
    // {
    //     var user = context.User.FirstOrDefault(u => u.Email.Equals(dto.Email));
    //     return user ?? userService.CreateEntity(dto);
    // }

    private CompanyUser AddCompanyUser(Company companyConcerned, Users users)
    {
        return context.CompanyUsers.Add(new CompanyUser
        {
            CompanyId = companyConcerned.Id,
            Company = companyConcerned,
            Users = users,
            UserId = users.Id,
            IsActive = true
        }).Entity;
    }
}