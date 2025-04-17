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
    private MdpCrypte _mdpCrypte = new MdpCrypte();
    protected async Task<Users> GetUser()
    {
        var accessToken = _mdpCrypte.Identified(cc.HttpContext?.GetTokenAsync("access_token").Result);
        var user = await context.User.FirstOrDefaultAsync(u => u.Email.Equals(accessToken) || u.Phone.Equals(accessToken));
        if (user == null)throw new Exception("User not found");
        return user;
    }

    protected async Task<Users> GetAdmin(Guid companyId)
    {
        var user = await GetUser(); 
        var userRoles = await context.UsersRoles
            .Include(ur => ur.Roles)
            .ThenInclude(r => r.Company) 
            .Where(ur => ur.UserId == user.Id && ur.Roles.Company.Id == companyId)
            .ToListAsync(); 

        
        var isAdmin = userRoles.Any(ur => ur.Roles.Name.Equals(EnumUsersRoles.SUPER_ADMIN.ToString()) || 
                                          ur.Roles.Name.Equals(EnumUsersRoles.ADMIN.ToString()));

        if (!isAdmin) 
            throw new Exception($"User {user.Email} is not an administrator");

        return user; 
    }

}