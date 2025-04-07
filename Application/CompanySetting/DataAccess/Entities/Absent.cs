
using Chameleon.Application.CompanySetting.Common.Enumeration;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.DataAccess.Entities;

public class Absent:BaseCompanyCard
{
    public Users? AcceptedBy { get; set; }
    public Guid? AcceptedById { get; set; }
    public EAbsentStatus Status { get; set; }
}