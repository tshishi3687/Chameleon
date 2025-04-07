using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.DataAccess.Entities;

public abstract class BaseCompanyCard: BaseEntity
{
    public Company Company { get; set; }
    public Guid CompanyId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public Users CreatedBy { get; set; }
    public Guid CreatedById { get; set; }
}