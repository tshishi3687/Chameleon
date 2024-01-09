namespace Chameleon.DataAccess.Entity;

public class Roles
{
    public Guid Id { get; }
    public string Name { get; set; }

    public Roles(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}