using System.ComponentModel.DataAnnotations;

namespace Chameleon.Business.Dto;

public class RolesDto
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "The Role name is required.")]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; }
}