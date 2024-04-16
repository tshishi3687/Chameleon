using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.DataAccess.Entities;

public class Company(Context context): BaseEntity
{
    public string Name { get; set; }
    public string BusinessNumber { get; set; }
    public ContactDetails ContactDetails { get; set; }
    public User Tutor { get; set; }

    public ICollection<CompanyUser> CompanyUser()
    {
        return context.CompanyUsers.Where(cu => cu.CompanyId.Equals(Id)).ToList();
    }

    public ICollection<CompanyCard> CompanyCards()
    {
        return context.CompanyCards.Where(cc => cc.Company.Id.Equals(Id)).ToList();
    }
}