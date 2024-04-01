using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.Common.DataAccess.Entities;

public class ContactDetails : BaseEntity
{
    public string Address { get; set; }
    public string Number { get; set; }
    public Locality Locality { get; set; }
    public Country Country { get; set; }
    
    public ICollection<Company> Companies { get; set; }
}