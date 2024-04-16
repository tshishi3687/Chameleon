using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Mappers;

namespace Chameleon.Application.CompanySetting.Business.Mappers;

public class TaskOrEventMapper: Mappers<TaskOrEventDto, TaskOrEvent>
{
    private readonly SimpleUserMapper _simpleUserMapper = new();
    
    public TaskOrEventDto ToDto(TaskOrEvent entity)
    {
        var taskOrEvent = new TaskOrEventDto
        {
            id = entity.Id,
            MadeBy = _simpleUserMapper.ToDto(entity.MadeBy),
            MadeById = entity.MadeById,
            Title = entity.Title,
            Description = entity.Description,
            Participant = new List<SimpleUserDto>()
        };

        foreach (var taskOrEventUser in entity.Participant())
        {
            taskOrEvent.Participant.Add(_simpleUserMapper.ToDto(taskOrEventUser.User));
        }

        return taskOrEvent;
    }

    public TaskOrEvent ToEntity(TaskOrEventDto dto)
    {
        throw new NotImplementedException();
    }

    public ICollection<TaskOrEventDto> ToDtos(ICollection<TaskOrEvent> entities)
    {
        return entities.Select(ToDto).ToList();
    }
}