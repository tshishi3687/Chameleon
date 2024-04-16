using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.Business.Services;

public class TaskOrEventService(Context context)
{
    public TaskOrEvent CreateEntity(TaskOrEventDto dto)
    {
        CheckDto(dto);
        var madeBy = context.User.FirstOrDefault(u => u.Id.Equals(dto.MadeById));
        var taskOrEvent = context.TaskOrEvents.Add(new TaskOrEvent(context)
        {
            MadeBy = madeBy!,
            MadeById = madeBy!.Id,
            Title = dto.Title,
            Description = dto.Description
        });

        var user = context.User.FirstOrDefault(u => u.Id.Equals(dto.MadeById));
        context.TaskOrEventUsers.Add(new TaskOrEventUser
        {
            TaskOrEven = taskOrEvent.Entity,
            TaskOrEventGuid = taskOrEvent.Entity.Id,
            User = user!,
            UserGuid = user!.Id
        });

        return taskOrEvent.Entity;
    }

    public TaskOrEvent ReadEntity(Guid guid)
    {
        var taskOrEvent = context.TaskOrEvents.FirstOrDefault(te => te.Equals(guid));
        if (taskOrEvent == null) throw new Exception("TaskOrEvent not found!");
        return taskOrEvent;
    }

    public ICollection<TaskOrEvent> ReadAllEntity()
    {
        return context.TaskOrEvents.ToList();
    }

    public TaskOrEvent UpdateEntity(TaskOrEventDto dto, Guid guid)
    {
        var taskOrEvent = context.TaskOrEvents.FirstOrDefault(te => te.Id.Equals(guid));
        if (taskOrEvent == null) throw new Exception("TaskOrEvent not found!");
        context.Remove(taskOrEvent);
        return CreateEntity(dto);
    }

    public void DeleteEntity(Guid guid)
    {
        var taskOrEvent = context.TaskOrEvents.FirstOrDefault(te => te.Id.Equals(guid));
        if (taskOrEvent == null) throw new Exception("TaskOrEvent not found!");
        context.Remove(taskOrEvent);
        context.SaveChanges();
    }

    private void CheckDto(TaskOrEventDto dto)
    {
        if (dto == null) throw new Exception("Dto cannot be null!");
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Memory name's cannot be null or empty!");
        if (string.IsNullOrEmpty(dto.Description)) throw new Exception("Memory description's cannot be null or empty!");
        var user = context.User.FirstOrDefault(u => u.Id.Equals(dto.MadeBy.Id));
        if (user == null) throw new Exception("Memory madeBy not found!");
    }
}