namespace Chameleon.DataAccess.Entity;

public class UsersRoles
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid RoleId { get; set; }
    public Roles Roles { get; set; }
}