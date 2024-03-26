namespace Chameleon.DataAccess.Entity;

public class Locality: BaseEntity
{
    public string Name { get; set; }
    public List<ContactDetails> ContactDetailsList { get; set; }
}