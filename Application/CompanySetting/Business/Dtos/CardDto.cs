namespace Chameleon.Application.CompanySetting.Business.Dtos;

public class CardDto
{
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public AbsentDto? AbsentDetails { get; set; }
    public MemoryDto? MemoryDetails { get; set; }
    public TasDto? Task { get; set; }
    public bool IsEnd { get; set; }
    public bool IsMade { get; set; }
}