using System.Runtime;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Mappers;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.Business.Services;

public class CompanyService(Context context) : IContext(context)
{
    private readonly CreationUserServiceBase _creationUserServiceBase = new(context);
    private readonly ContactDetailsServiceBase _contactDetailsServiceBase = new(context);
    private readonly CompanyEasyVueMapper _companyEasyVueMapper = new();

    public CompanyEasyVueDto CreateEntity(CreationCompanyDto dto)
    {
        CheckIsNullOrWhiteSpace(dto);

        var contactDetails = Context.ContactDetails.SingleOrDefault(c =>
            c.Id.Equals(_contactDetailsServiceBase.CreateEntity1(dto.ContactDetail).Id));
        var user = AddUser(dto);

        if (contactDetails == null)
        {
            throw new Exception(); // TODO
        }

        // Add in table Company
        var lastEntity = Context.Companies.Add(new Company
        {
            Name = dto.Name,
            BusinessNumber = dto.BusinessNumber,
            ContactDetails = contactDetails,
            Tutor = user,
            Users = new List<CompanyUser>()
        });

        // Add in table CompanyUser
        Context.CompanyUsers.Add(new CompanyUser
        {
            CompanyId = lastEntity.Entity.Id,
            UserId = user.Id
        });

        // Add in table UserRoles
        Context.UsersRoles.Add(new UsersRoles
        {
            UserId = user.Id,
            RoleId = new Roles
            {
                Name = EnumUsersRoles.SUPER_ADMIN.ToString(),
                Company = lastEntity.Entity
            }.Id
        });

        // Save All insert.
        Context.SaveChanges();

        return _companyEasyVueMapper.ToDto(Context.Companies.Last());
    }

    private void CheckIsNullOrWhiteSpace(CreationCompanyDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new AmbiguousImplementationException("Dto name's can't be null!");
        }

        if (string.IsNullOrWhiteSpace(dto.BusinessNumber))
        {
            throw new AmbiguousImplementationException("Dto business number's can't be null!");
        }

        if (dto.UserId == null && dto.Tutor == null)
        {
            throw new AmbiguousImplementationException("Can't create or found user");
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
        var userDto = _creationUserServiceBase.CreateEntity1(dto);
        var userEntity = Context.User.FirstOrDefault(u => u.Id.Equals(userDto.Id));
        if (userEntity == null)
        {
            throw new FileNotFoundException("User not found!");
        }

        return userEntity;
    }

    private User AddUser(CreationCompanyDto dto)
    {
        return dto switch
        {
            { UserId: not null, Tutor: not null } => TakeUserInContext(dto.UserId.Value),
            { UserId: not null } => TakeUserInContext(dto.UserId.Value),
            _ => TakeUserThroughCreation(dto.Tutor!)
        };
    }
}