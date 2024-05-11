using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Chameleon.Application.HumanSetting.Business.Dtos;

namespace Chameleon.Application.CompanySetting.Business.Dtos;

public class CreationCompanyAndUserDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? BusinessNumber { get; set; }
    
    public CreationUserDto? UserDto { get; set; }
}