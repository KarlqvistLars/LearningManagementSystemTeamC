namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record ModuleDto(
    Guid Id,
    string ModuleName,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    Guid CourseId,
    string CourseName
    );