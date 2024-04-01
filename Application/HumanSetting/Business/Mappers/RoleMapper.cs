using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.HumanSetting.Business.Mappers;

public class RoleMapper: Mappers<RolesDto, Roles>
{
    public RolesDto ToDto(Roles entity)
    {
        return new RolesDto { Id = entity.Id, Name = entity.Name };
    }

    public Roles toEntity(RolesDto dto)
    {
        return new Roles { Name = dto.Name };
    }

    public ICollection<RolesDto> toDtos(ICollection<Roles> entities)
    {
        return entities.Select(entity => ToDto(entity)).ToList();
    }
}