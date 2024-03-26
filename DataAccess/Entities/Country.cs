namespace Chameleon.DataAccess.Entity;

public class Country : BaseEntity
{
    public string Name { get; set; }
    public List<ContactDetails> ContactDetailsList { get; set; }
}