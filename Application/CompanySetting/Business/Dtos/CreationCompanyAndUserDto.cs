using System.ComponentModel.DataAnnotations;
using Chameleon.Application.HumanSetting.Business.Dtos;

namespace Chameleon.Application.CompanySetting.Business.Dtos;

public class CreationCompanyAndUserDto
{
    [Required] public string Name { get; set; }
    [Required] public string BusinessNumber { get; set; }
    [Required] public bool isVisible { get; set; }
    [Required] public CreationUserDto UserDto { get; set; }
}