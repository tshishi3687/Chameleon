using Chameleon.Application.CompanySetting.Common.Enumeration;
using Chameleon.Application.HumanSetting.Business.Dtos;

namespace Chameleon.Application.CompanySetting.Business.Dtos;

public class TasDto
{
    public Guid id { get; set; }
    public CardType CardType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsEnd { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public SimpleUserDto CreatedBy { get; set; }
    public ICollection<SimpleUserDto> Participant { get; set; }
}