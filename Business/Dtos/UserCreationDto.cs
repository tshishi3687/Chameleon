using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Chameleon.Business.Dto;

public class UserCreationDto
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string FirstName { get; set; }
    
    [MinLength(2)]
    [MaxLength(50)]
    [Required(ErrorMessage = "The LastName is required.")]
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "The first name must not contain numbers.")]
    public string LastName { get; set; }
    
    [Required]
    public DateTime BursDateTime { get; set; }
    
    [Required(ErrorMessage = "The Email is required")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "The email address is not in a valid format.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "The phone number is required.")]
    [RegularExpression(@"^\+32\d{9}$", ErrorMessage = "The telephone number must be Belgian.")]
    public string Phone { get; set; }
    
    [Required]
    [MinLength(8)]
    [MaxLength(32)]
    public string PassWord { get; set; }
    
    [Required]
    public string PassWordCheck { get; set; }
    public Collection<RolesDto> Roles { get; set; }
}