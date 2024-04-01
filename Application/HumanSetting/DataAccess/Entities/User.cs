namespace Chameleon.Application.HumanSetting.DataAccess.Entities;

public class User : BaseEntity
{
    public Guid ReferenceCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BursDateTime { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PassWord { get; set; }
    public ICollection<UsersRoles> Roles { get; set; }
    public ICollection<UsersContactDetails> ContactDetails { get; set; }
}