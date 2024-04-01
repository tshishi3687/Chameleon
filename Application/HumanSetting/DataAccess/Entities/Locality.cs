namespace Chameleon.Application.HumanSetting.DataAccess.Entities;

public class Locality: BaseEntity
{
    public string Name { get; set; }
    public ICollection<ContactDetails> ContactDetailsList { get; set; }
}