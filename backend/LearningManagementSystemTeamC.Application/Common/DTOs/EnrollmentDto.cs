namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record EnrollmentDto(
    Guid Id,
    Guid UserId,
    Guid CourseId,
    DateTime EnrolledAt,
    bool IsActive
);
