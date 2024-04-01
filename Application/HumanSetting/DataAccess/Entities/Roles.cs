using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.CompanySetting.DataAccess.Entities;

namespace Chameleon.Application.HumanSetting.DataAccess.Entities;

public class Roles : BaseEntity
{
    public EnumUsersRoles Name { get; set; }
    public Company Company { get; set; }
    public ICollection<UsersRoles> Users { get; set; }

}