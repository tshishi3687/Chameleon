using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Chameleon.Application.CompanySetting.DataAccess.Entities.Task;

namespace Chameleon.Application.CompanySetting.DataAccess.EntityConfig;

public class TaskConfig: IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable("Task");

        builder.HasKey(x => x.Id).HasName("PK_company");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(te => te.Title).IsRequired();
        builder.Property(te => te.Description).IsRequired();
        
        builder.HasOne(te => te.CreatedBy)
            .WithOne()
            .HasForeignKey<Task>(te => te.CreatedById);
    }
}