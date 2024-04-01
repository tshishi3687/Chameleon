namespace Chameleon.Application.HumanSetting.DataAccess.Entities;

public class Roles : BaseEntity
{
    public string Name { get; set; }
    public ICollection<UsersRoles> Users { get; set; }

}