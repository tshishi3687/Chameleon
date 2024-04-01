namespace Chameleon.Application.HumanSetting.DataAccess.Entities;

public class UsersContactDetails
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid ContactDetailsId { get; set; }
    public ContactDetails ContactDetails { get; set; }
}