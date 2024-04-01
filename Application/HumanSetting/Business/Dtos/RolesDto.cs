using System.ComponentModel.DataAnnotations;

namespace Chameleon.Application.HumanSetting.Business.Dtos;

public class RolesDto
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "The Role name is required.")]
    [MinLength(2)]
    [MaxLength(50)]
    public EnumUsersRoles Name { get; set; }
}