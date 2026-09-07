using LearningManagementSystemTeamC.Domain.Enrollments;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Configurations;

public class EnrollmentConfigutation : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Enrollment> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId).IsRequired().HasMaxLength(EnrollmentRules.UserIdMaxLength);
        // endast en anmälan per elev och kurs
        builder.HasIndex(e => e.UserId).IsUnique(true);

        builder.Property(e => e.CourseId).IsRequired();

        builder.Property(e => e.EnrolledAt).IsRequired();

        builder.Property(e => e.IsActive).IsRequired();
    }
}
