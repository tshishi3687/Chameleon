namespace Chameleon.Application.CompanySetting.DataAccess.Entities;

public class CompanyCard
{
    public Guid CompanyGuid { get; set; }
    public Company Company { get; set; }
    public Guid CardGuid { get; set; }
    public Card Card { get; set; }
}