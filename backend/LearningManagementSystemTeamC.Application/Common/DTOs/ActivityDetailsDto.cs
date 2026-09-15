namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record ActivityDetailsDto(
    Guid Id,
    string ActivityName,
    int Type,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    Guid ModuleId,
    string ModuleName,
    Guid CourseId,
    string CourseName,
    int SubmittedCount,
    int TotalStudents
);