using Chameleon.Application.CompanySetting.Common.Enumeration;
using Chameleon.Application.HumanSetting.Business.Dtos;

namespace Chameleon.Application.CompanySetting.Business.Dtos;

public class AbsentDto
{
    public Guid Id { get; set; }
    public SimpleUserDto ForUsers { get; set; }
    public EAbsentStatus Status { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}