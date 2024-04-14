using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.HumanSetting.Business.Mappers;

public class SimpleUserMapper: Mappers<SimpleUserDto, User>
{
    public SimpleUserDto ToDto(User entity)
    {
        return new SimpleUserDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName
        };
    }

    public User ToEntity(SimpleUserDto dto)
    {
        throw new NotImplementedException();
    }

    public ICollection<SimpleUserDto> ToDtos(ICollection<User> entities)
    {
        return entities.Select(ToDto).ToList();
    }
}