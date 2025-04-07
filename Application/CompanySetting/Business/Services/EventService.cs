using Task = Chameleon.Application.CompanySetting.DataAccess.Entities.Task;

namespace Chameleon.Application.CompanySetting.Business.Services;

public class EventService(Context context)
{
    public Task ReadEntity(Guid guid)
    {
        var taskOrEvent = context.Tasks.FirstOrDefault(te => te.Equals(guid));
        if (taskOrEvent == null) throw new Exception("TaskOrEvent not found!");
        return taskOrEvent;
    }

    public ICollection<Task> ReadAllEntity()
    {
        return context.Tasks.ToList();
    }

    public void DeleteEntity(Guid guid)
    {
        var taskOrEvent = context.Tasks.FirstOrDefault(te => te.Id.Equals(guid));
        if (taskOrEvent == null) throw new Exception("TaskOrEvent not found!");
        context.Remove(taskOrEvent);
        context.SaveChanges();
    }
}