using Chameleon.Business.Dtos;
using Chameleon.DataAccess.Entity;

namespace Chameleon.Business.Mappers;

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

    public Locality toEntity(LocalityDto dto)
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