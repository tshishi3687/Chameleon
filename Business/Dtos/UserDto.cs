using Chameleon.Business.Dto;

namespace Chameleon.Business.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BursDateTime { get; set; }
    public string EmailEncoding { get; set; }
    public string PhoneEncoding { get; set;  }
    public List<RolesDto> Roles { get; set; }
}