using Chameleon.Application.HumanSetting.Business.Dtos;

namespace Chameleon.Application.CompanySetting.Business.Dtos;

public class AddCompanyUser
{
    public string? Identification { get; set; }
    public CreationUserDto? CreationUserDto { get; set; }
}