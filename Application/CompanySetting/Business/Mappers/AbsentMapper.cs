using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.Business.Mappers;

namespace Chameleon.Application.CompanySetting.Business.Mappers;

public class AbsentMapper: Mappers<AbsentDto, Absent>
{
    private readonly SimpleUserMapper _simpleUserMapper = new();
    
    public AbsentDto ToDto(Absent entity)
    {
        return new AbsentDto
        {
            Id = entity.Id,
            ForUsers = _simpleUserMapper.ToDto(entity.CreatedBy),
            Status = entity.Status,
            From = entity.From,
            To = entity.To,
        };
    }

    public Absent ToEntity(AbsentDto dto)
    {
        throw new NotImplementedException();
    }

    public ICollection<AbsentDto> ToDtos(ICollection<Absent> entities)
    {
        return entities.Select(ToDto).ToList();
    }
}