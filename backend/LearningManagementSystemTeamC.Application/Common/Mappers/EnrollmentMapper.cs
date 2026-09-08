using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.Enrollments;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class EnrollmentMapper
{
    public static EnrollmentDto EnrollmentToDto(Enrollment enrollment) =>
        new EnrollmentDto(
                enrollment.Id,
                enrollment.UserId,
                enrollment.CourseId,
                enrollment.EnrolledAt,
                enrollment.IsActive);
}
