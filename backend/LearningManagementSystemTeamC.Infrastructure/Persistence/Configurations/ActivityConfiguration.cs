using LearningManagementSystemTeamC.Domain.Activities;
using LearningManagementSystemTeamC.Domain.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ActivityName)
            .IsRequired()
            .HasMaxLength(ActivityRules.ActivityNameMaxLength);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(ActivityRules.DescriptionMaxLength);

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.HasOne<Module>()
            .WithMany()
            .HasForeignKey(x => x.ModuleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
