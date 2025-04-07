namespace Chameleon.Application.CompanySetting.Business.Dtos;

public class CompanyEasyVueDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string BusinessNumber { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] FileContent { get; set; }
}