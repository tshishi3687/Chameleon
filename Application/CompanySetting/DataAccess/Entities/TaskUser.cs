using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.DataAccess.Entities;

public class TaskUser
{
    public Task Task { get; set; }
    public Guid TaskId { get; set; }
    public Users Users { get; set; }
    public Guid UserId { get; set; }
}