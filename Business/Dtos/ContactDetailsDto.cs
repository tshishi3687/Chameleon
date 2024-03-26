namespace Chameleon.Business.Dtos;

public class ContactDetailsDto
{
    public Guid Id { get; set; }
    public string Address { get; set; }
    public string Number { get; set; }
    public LocalityDto Locality { get; set; }
    public CountryDto Country { get; set; }
    
}