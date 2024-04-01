using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.DataAccess.Entities;

public class Company: BaseEntity
{
    public string Name { get; set; }
    public string BusinessNumber { get; set; }
    public ContactDetails ContactDetails { get; set; }
    public User Tutor { get; set; }
    public ICollection<CompanyUser> Users { get; set; }
}