using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Task = Chameleon.Application.CompanySetting.DataAccess.Entities.Task;

namespace Chameleon.Application.HumanSetting.DataAccess.Entities;

public class Users(Context context) : BaseEntity
{
    public Guid ValidationCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BursDateTime { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PassWord { get; set; }
    public ICollection<UsersRoles> Roles { get; set; }

    public async Task<ICollection<UsersRoles>> UserRoles()
    {
        return await context.UsersRoles.Where(ur => ur.UserId.Equals(Id)).ToListAsync();
    }

    public async Task<ICollection<UsersContactDetails>> UserContactDetails()
    {
        return await context.UsersContactDetails.Where(ur => ur.UserId.Equals(Id)).ToListAsync();
    }

    public async Task<ICollection<CompanyUser>> Companies()
    {
        return await context.CompanyUsers.Where(ur => ur.UserId.Equals(Id)).ToListAsync();
    }
}