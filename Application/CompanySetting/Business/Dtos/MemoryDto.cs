using Chameleon.Application.HumanSetting.Business.Dtos;

namespace Chameleon.Application.CompanySetting.Business.Dtos;

public class MemoryDto
{
    public Guid Id { get; set; }
    public SimpleUserDto MadeBy { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
}