namespace Chameleon.Application.HumanSetting.DataAccess.Entities;

public class Country : BaseEntity
{
    public string Name { get; set; }
    public ICollection<ContactDetails> ContactDetailsList { get; set; }
}