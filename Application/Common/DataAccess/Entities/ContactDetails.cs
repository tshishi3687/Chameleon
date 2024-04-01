namespace Chameleon.Application.HumanSetting.DataAccess.Entities;

public class ContactDetails : BaseEntity
{
    public string Address { get; set; }
    public string Number { get; set; }
    public Locality Locality { get; set; }
    public Country Country { get; set; }
}