using Chameleon.Application.HumanSetting.Business.Dtos;

namespace Chameleon.Application.CompanySetting.Business.Dtos;

public class TaskOrEventDto
{
    public Guid id { get; set; }
    public SimpleUserDto MadeBy { get; set; }
    public Guid MadeById { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<SimpleUserDto> Participant { get; set; }
}