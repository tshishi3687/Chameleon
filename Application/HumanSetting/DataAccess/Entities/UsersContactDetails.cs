using Chameleon.Application.Common.DataAccess.Entities;

namespace Chameleon.Application.HumanSetting.DataAccess.Entities;

public class UsersContactDetails
{
    public Guid UserId { get; set; }
    public Users Users { get; set; }
    public Guid ContactDetailsId { get; set; }
    public ContactDetails ContactDetails { get; set; }
}