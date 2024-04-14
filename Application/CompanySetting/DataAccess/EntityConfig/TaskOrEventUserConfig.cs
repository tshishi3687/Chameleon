using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.CompanySetting.DataAccess.EntityConfig;

public class TaskOrEventUserConfig : IEntityTypeConfiguration<TaskOrEventUser>
{
    public void Configure(EntityTypeBuilder<TaskOrEventUser> builder)
    {
        builder.HasKey(x => new { x.TaskOrEventGuid, x.UserGuid });
    }
}