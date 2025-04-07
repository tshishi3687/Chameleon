namespace Chameleon.Application.HumanSetting.DataAccess.Entities;

public class UsersRoles
{
    public Guid UserId { get; set; }
    public Users Users { get; set; }
    public Guid RoleId { get; set; }
    public Roles Roles { get; set; }
}