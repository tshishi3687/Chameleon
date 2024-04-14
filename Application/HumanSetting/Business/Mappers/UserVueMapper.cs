using Chameleon.Application.Common.Business.Mappers;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.HumanSetting.Business.Mappers;

public class UserVueMapper : Mappers<UserVueDto, User>
{
    
    public UserVueDto ToDto(User entity)
    {

        return new UserVueDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            BursDateTime = entity.BursDateTime,
            ContactDetails = entity.UserContactDetails().Select(uc => new ContactDetailsMapper().ToDto(uc.ContactDetails)).ToList(),
            Roles = entity.UserRoles().Select(ur => new RoleMapper().ToDto(ur.Roles)).ToList()
        };
    }

    public User ToEntity(UserVueDto vueDto)
    {
        throw new NotImplementedException();
    }

    public ICollection<UserVueDto> ToDtos(ICollection<User> entities)
    {
        return entities.Select(ToDto).ToList();
    }
}