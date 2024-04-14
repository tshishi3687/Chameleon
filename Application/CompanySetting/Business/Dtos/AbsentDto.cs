using Chameleon.Application.HumanSetting.Business.Dtos;

namespace Chameleon.Application.CompanySetting.Business.Dtos;

public class AbsentDto
{
    public Guid Id { get; set; }
    public SimpleUserDto MadeBy { get; set; }
}