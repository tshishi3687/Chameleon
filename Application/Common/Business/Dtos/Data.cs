using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.HumanSetting.Business.Dtos;

public class Data
{
    public string Token { get; set; }
    public string UserName { get; set; }
    public string CompanyName { get; set; }
    public Guid Reference { get; set; }
    public List<string>  Roles { get; set; }
}