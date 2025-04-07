namespace Chameleon.Application.CompanySetting.Business.Dtos;

public class CompanyPictureDto
{
    public string FileName { get; set; }
    public string? ContentType { get; set; }
    public byte[] FileContent { get; set; }
}