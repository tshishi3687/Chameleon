namespace Chameleon.DataAccess.Entity;

public class Roles : BaseEntity
{
    public string Name { get; set; }
    public ICollection<UsersRoles> Users { get; set; }

}