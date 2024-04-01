namespace Chameleon.Application.HumanSetting.Business.Dtos;

public class UserVueDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BursDateTime { get; set; }
    public ICollection<RolesDto> Roles { get; set; }
    public ICollection<ContactDetailsDto> ContactDetails { get; set; }
}