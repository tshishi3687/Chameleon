namespace Chameleon.DataAccess.Entity;

public class Roles
{
    public Guid Id { get; }
    public string Name { get; set; }
    public ICollection<UsersRoles> Users { get; set; }

}