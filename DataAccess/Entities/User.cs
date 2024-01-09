namespace Chameleon.DataAccess.Entity;

public class User
{
    public Guid Id { get; }
    public Guid ReferenceCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BursDateTime { get; set; }
    public string Email { get; set; }
    public string EmailEncoding { get; set; }
    public string Phone { get; set; }
    public string PhoneEncoding { get; set; }
    public string PassWord { get; set; }
    public ICollection<UsersRoles> Roles { get; set; }
}