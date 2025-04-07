namespace Chameleon.Application.Common.Business.Dtos;

public class Passport
{
    public string Token { get; set; }
    public string UserName { get; set; }
    public string CompanyName { get; set; }
    public Guid Reference { get; set; }
    public List<string>  Roles { get; set; }
}