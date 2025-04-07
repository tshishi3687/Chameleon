using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Mappers;
using Task = Chameleon.Application.CompanySetting.DataAccess.Entities.Task;

namespace Chameleon.Application.CompanySetting.Business.Mappers;

public class TaskMapper: Mappers<TasDto, Task>
{
    private readonly SimpleUserMapper _simpleUserMapper = new();
    
    public TasDto ToDto(Task entity)
    {
        var taskOrEvent = new TasDto
        {
            id = entity.Id,
            CardType = entity.CardType,
            Title = entity.Title,
            Description = entity.Description,
            From = entity.From,
            To = entity.To,
            IsEnd = entity.IsEnd,
            Participant = new List<SimpleUserDto>()
        };

        return taskOrEvent;
    }

    public Task ToEntity(TasDto dto)
    {
        throw new NotImplementedException();
    }

    public ICollection<TasDto> ToDtos(ICollection<Task> entities)
    {
        return entities.Select(ToDto).ToList();
    }
}