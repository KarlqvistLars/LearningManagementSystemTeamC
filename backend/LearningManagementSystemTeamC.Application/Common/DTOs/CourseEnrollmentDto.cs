namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record CourseEnrollmentDto(
    Guid StudentId,
    DateTime EnrollmentDate,
    string StudentName
    );