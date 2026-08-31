using LearningManagementSystemTeamC.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);

        // No magic string or number, use const, store it in
        // for example CourseRules in Feature folders in Application project
        // Application/Courses/CourseRules.cs

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