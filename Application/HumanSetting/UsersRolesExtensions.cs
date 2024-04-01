namespace Chameleon.Application.HumanSetting;

public enum EnumUsersRoles
{
    SUPER_ADMIN,
    ADMIN,
    WORKER,
    CUSTOMER
}

public static class UsersRolesExtensions
{
    public static string GetRoleName(this EnumUsersRoles role)
    {
        switch (role)
        {
            case EnumUsersRoles.SUPER_ADMIN:
                return "SUPER ADMIN";
            case EnumUsersRoles.ADMIN:
                return "ADMIN";
            case EnumUsersRoles.WORKER:
                return "WORKER";
            case EnumUsersRoles.CUSTOMER:
                return "CUSTOMER";
            default:
                throw new ArgumentException("Unknown user role.");
        }
    }
}
