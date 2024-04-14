using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chameleon.Application.CompanySetting.DataAccess.EntityConfig;

public class TaskOrEventConfig: IEntityTypeConfiguration<TaskOrEvent>
{
    public void Configure(EntityTypeBuilder<TaskOrEvent> builder)
    {
        builder.ToTable("TaskOrEvent");

        builder.HasKey(x => x.Id).HasName("PK_company");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(te => te.Title).IsRequired();
        builder.Property(te => te.Description).IsRequired();
        
        builder.HasOne(te => te.MadeBy)
            .WithOne()
            .HasForeignKey<TaskOrEvent>(te => te.MadeById);
    }
}