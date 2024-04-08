using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.HumanSetting.Business.Mappers;

public class LocalityMapper: Mappers<LocalityDto, Locality>
{
    public LocalityDto ToDto(Locality entity)
    {
        return new LocalityDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public Locality ToEntity(LocalityDto dto)
    {
        return new Locality
        {
            Name = dto.Name.ToUpper()
        };
    }

    public ICollection<LocalityDto> toDtos(ICollection<Locality> entities)
    {
        return entities.Select(entity => ToDto(entity)).ToList();
    }
}