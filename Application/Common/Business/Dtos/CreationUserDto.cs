using System.ComponentModel.DataAnnotations;

namespace Chameleon.Application.HumanSetting.Business.Dtos;

public class CreationUserDto
{
    [Required(ErrorMessage = "FirstName")]
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "FirstName")]
    [MinLength(2)]
    [MaxLength(50)]
    public string? FirstName { get; set; }
    
    [Required(ErrorMessage = "LastName")]
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "LastName")]
    [MinLength(2)]
    [MaxLength(50)]
    public string? LastName { get; set; }
    
    [Required]
    public DateTime BursDateTime { get; set; }
    
    [Required(ErrorMessage = "Email")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Phone")]
    [RegularExpression(@"^\+32\d{9}$", ErrorMessage = "Phone")]
    public string Phone { get; set; }
    
    [Required(ErrorMessage = "PassWord")]
    [MinLength(8, ErrorMessage = "PassWord")]
    [MaxLength(32, ErrorMessage = "PassWord")]
    public string PassWord { get; set; }

    [Required(ErrorMessage = "PassWordCheck")]
    [Compare(nameof(PassWord), ErrorMessage = "PassWordCheck")]
    public string PassWordCheck { get; set; }
    
    public List<ContactDetailsDto>? ContactDetails { get; set; }
    
    public List<EnumUsersRoles>? Roles { get; set; }
}