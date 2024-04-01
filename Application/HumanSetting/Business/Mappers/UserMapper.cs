using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.HumanSetting.Business.Mappers;

public class UserMapper : Mappers<UserVueDto, User>
{
    
    public UserVueDto ToDto(User entity)
    {

        return new UserVueDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            BursDateTime = entity.BursDateTime,
            ContactDetails = new List<ContactDetailsDto>()
        };
    }

    public User toEntity(UserVueDto vueDto)
    {
        throw new NotImplementedException();
    }

    public ICollection<UserVueDto> toDtos(ICollection<User> entities)
    {
        return entities.Select(ToDto).ToList();
    }
}