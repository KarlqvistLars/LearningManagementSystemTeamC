using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.Enrollments;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class EnrollmentMapper
{
    public static CourseEnrollmentDto CourseEnrollmentToDto(Enrollment enrollment, string name) =>
        new CourseEnrollmentDto(
            enrollment.UserId,
            enrollment.EnrolledAt,
            name);

    public static EnrollmentDto EnrollmentToDto(Enrollment enrollment) =>
        new EnrollmentDto(
            enrollment.UserId,
            enrollment.CourseId,
            enrollment.EnrolledAt);
}