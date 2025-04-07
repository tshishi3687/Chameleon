using Chameleon.Application.CompanySetting.Common.Enumeration;
using Chameleon.Application.CompanySetting.Common.Interface;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.DataAccess.Entities;

public class Task: BaseCompanyCard, ITitleDescription
{
    public CardType CardType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsEnd { get; set; }
    public ICollection<Users> Participant { get; set; } = new List<Users>();
}