namespace Chameleon.Application.HumanSetting.Business.Dtos;

public class SimpleUserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public List<string> Roles { get; set; } // Liste des noms de rôles
}