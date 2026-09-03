using LearningManagementSystemTeamC.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CourseName)
            .IsRequired()
            .HasMaxLength(CourseRules.CourseNameMaxLength);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(CourseRules.DescriptionMaxLength);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}