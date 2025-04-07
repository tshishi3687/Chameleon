using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.CompanySetting.DataAccess.EntityConfig;

public class TaskUserConfig : IEntityTypeConfiguration<TaskUser>
{
    public void Configure(EntityTypeBuilder<TaskUser> builder)
    {
        builder.HasKey(x => new { x.TaskId, x.UserId });
    }
}