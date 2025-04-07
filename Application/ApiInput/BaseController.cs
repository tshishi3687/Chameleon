using Chameleon.Application.HumanSetting;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chameleon.Application.ApiInput;

[ApiController]
[Route("[controller]")]
public abstract class BaseController(IHttpContextAccessor cc, Context context): Controller
{
    
    protected async Task<Users> GetUser()
    {
        var accessToken = cc.HttpContext?.GetTokenAsync("access_token").Result;
        var user = await context.User.FirstOrDefaultAsync(u => u.Email.Equals(accessToken) || u.Phone.Equals(accessToken));
        if (user == null)throw new Exception("User not found");
        return user;
    }

    protected async Task<Users> GetAdmin(Guid companyId)
    {
        var user = await GetUser();
        var roles  = await context.UsersRoles
            .AnyAsync(ur => ur.UserId.Equals(user.Id) && ur.Roles.Company.Id.Equals(companyId) && (ur.Roles.Name.Equals(EnumUsersRoles.SUPER_ADMIN) || ur.Roles.Name.Equals(EnumUsersRoles.ADMIN)));
        if (!roles) throw new Exception($"User {user.Email} is not an administrator");
        return user;
    }
}