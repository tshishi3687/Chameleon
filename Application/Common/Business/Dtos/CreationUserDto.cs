using System.ComponentModel.DataAnnotations;

namespace Chameleon.Application.HumanSetting.Business.Dtos;

public class CreationUserDto
{
    [Required(ErrorMessage = "The FirstName is required.")]
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "The FirstName must not contain numbers.")]
    [MinLength(2)]
    [MaxLength(50)]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "The LastName is required.")]
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "The LastNAme must not contain numbers.")]
    [MinLength(2)]
    [MaxLength(50)]
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
    
    public List<ContactDetailsDto> ContactDetails { get; set; }
    
    public List<EnumUsersRoles> Roles { get; set; }
}