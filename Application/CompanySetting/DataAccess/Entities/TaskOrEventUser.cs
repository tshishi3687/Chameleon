using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.DataAccess.Entities;

public class TaskOrEventUser
{
    public TaskOrEvent TaskOrEven { get; set; }
    public Guid TaskOrEventGuid { get; set; }
    public User User { get; set; }
    public Guid UserGuid { get; set; }
}