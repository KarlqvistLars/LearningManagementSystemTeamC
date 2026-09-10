using LearningManagementSystemTeamC.Domain.Activities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.ActivityName)
            .IsRequired()
            .HasMaxLength(ActivityRules.ActivityNameMaxLength);

        builder.Property(a => a.Description)
            .HasMaxLength(ActivityRules.DescriptionMaxLength);

        builder.Property(a => a.StartDate)
            .IsRequired();

        builder.Property(a => a.EndDate)
            .IsRequired();

        builder.Property(a => a.Type)
            .IsRequired();

        builder.Property(a => a.ModuleId)
            .IsRequired();
    }
}
