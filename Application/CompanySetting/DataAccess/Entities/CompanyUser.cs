using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.DataAccess.Entities;

public class CompanyUser
{
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public Guid UserId { get; set; }
    public Users Users { get; set; }
    public bool IsActive { get; set; }
}