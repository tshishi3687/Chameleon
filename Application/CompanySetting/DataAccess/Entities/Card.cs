using Chameleon.Application.Common.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.DataAccess.Entities;

public class Card: BaseEntity
{
    public DateTime DateTime { get; set; }
    public Absent AbsentDetails { get; set; }
    public Guid AbsentDetailsId { get; set; }
    public Memory MemoryDetails { get; set; }
    public Guid MemoryDetailsId { get; set; }
    public TaskOrEvent TaskOrEventDetails { get; set; }
    public Guid TaskOrEventDetailsId { get; set; }
    public bool? IsEnd { get; set; }
    public bool? IsMadeIt { get; set; }
}