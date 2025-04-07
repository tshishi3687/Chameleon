using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chameleon.Application.CompanySetting.DataAccess.Entities;

public class Company(Context context): BaseEntity
{
    public string Name { get; set; }
    public string BusinessNumber { get; set; }
    public Users Tutor { get; set; }
    public bool IsActive { get; set; }
    public string FileName { get; set; }
    public string? ContentType { get; set; }
    public byte[] FileContent { get; set; }
    public async Task<ICollection<CompanyUser>> CompanyUser()
    {
        return await context.CompanyUsers.Where(cu => cu.CompanyId.Equals(Id)).ToListAsync();
    }

}