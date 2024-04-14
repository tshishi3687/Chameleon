using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.Business.Mappers;

namespace Chameleon.Application.CompanySetting.Business.Mappers;

public class MemoryMapper: Mappers<MemoryDto, Memory>
{
    private readonly SimpleUserMapper _simpleUserMapper = new();
    
    public MemoryDto ToDto(Memory entity)
    {
        return new MemoryDto
        {
            Id = entity.Id,
            MadeBy = _simpleUserMapper.ToDto(entity.MadeBy),
            Title = entity.Title,
            Description = entity.Description
        };
    }

    public Memory ToEntity(MemoryDto dto)
    {
        throw new NotImplementedException();
    }

    public ICollection<MemoryDto> ToDtos(ICollection<Memory> entities)
    {
        return entities.Select(ToDto).ToList();
    }
}