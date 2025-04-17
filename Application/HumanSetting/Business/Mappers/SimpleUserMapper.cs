using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.HumanSetting.Business.Mappers;

public class SimpleUserMapper: Mappers<SimpleUserDto, Users>
{
    public SimpleUserDto ToDto(Users entity)
    {
        if (entity == null) return null;

        var roles = entity.Roles.Select(entityRole => entityRole.Roles.Name).ToList();
        return new SimpleUserDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Phone = entity.Phone,
            Roles = roles,
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