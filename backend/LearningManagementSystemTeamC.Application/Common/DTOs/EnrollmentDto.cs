namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record EnrollmentDto(
    Guid StudentId,
    Guid CourseId,
    DateTime EnrollmentDate
    );