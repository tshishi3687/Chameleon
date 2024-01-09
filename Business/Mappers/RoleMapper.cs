using Chameleon.Business.Dto;
using Chameleon.DataAccess.Entity;

namespace Chameleon.Business.Mappers;

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
}