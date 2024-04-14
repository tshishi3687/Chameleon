using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.CompanySetting.Common.Interface;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.DataAccess.Entities;

public class TaskOrEvent(Context context): BaseEntity, ICardType, ITitleDescription
{
    public User MadeBy { get; }
    public Guid MadeById { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public ICollection<TaskOrEventUser> Participant()
    {
        return context.TaskOrEventUsers.Where(tu => tu.UserGuid.Equals(Id)).ToList();
    }
}