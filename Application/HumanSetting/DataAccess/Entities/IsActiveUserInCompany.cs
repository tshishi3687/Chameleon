namespace Chameleon.Application.HumanSetting.DataAccess.Entities;

public class IsActiveUserInCompany
{
    public bool IsActive { get; set; }
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
}