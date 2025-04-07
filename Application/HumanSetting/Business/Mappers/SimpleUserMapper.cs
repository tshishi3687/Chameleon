using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.HumanSetting.Business.Mappers;

public class SimpleUserMapper: Mappers<SimpleUserDto, Users>
{
    public SimpleUserDto ToDto(Users entity)
    {
        return new SimpleUserDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName
        };
    }

    public Users ToEntity(SimpleUserDto dto)
    {
        throw new NotImplementedException();
    }

    public ICollection<SimpleUserDto> ToDtos(ICollection<Users> entities)
    {
        return entities.Select(ToDto).ToList();
    }
}