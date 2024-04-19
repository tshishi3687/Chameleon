using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.CompanySetting.DataAccess.Entities;

namespace Chameleon.Application.HumanSetting.DataAccess.Entities;

public class User(Context context) : BaseEntity
{
    public Guid ValidationCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BursDateTime { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PassWord { get; set; }

    public ICollection<IsActiveUserInCompany> IsActiveInCompanies(Guid companyId)
    {
        return context.IsActiveUserInCompanies.Where(i => i.CompanyId.Equals(companyId)).ToList();
    }
    
    public ICollection<UsersRoles> UserRoles()
    {
        return context.UsersRoles.Where(ur => ur.UserId.Equals(Id)).ToList();
    }

    public ICollection<UsersContactDetails> UserContactDetails()
    {
        return context.UsersContactDetails.Where(ur => ur.UserId.Equals(Id)).ToList();
    }

    public ICollection<CompanyUser> Companies()
    {
        return context.CompanyUsers.Where(ur => ur.UserId.Equals(Id)).ToList();
    }
}