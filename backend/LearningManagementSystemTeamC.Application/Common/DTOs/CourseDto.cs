namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record CourseDto(
    Guid Id,
    string CourseName,
    string Description,
    DateTime StartDate,
    DateTime EndDate
    );

public record CourseCreateDto(
    string CourseName,
    string Description,
    DateTime StartDate,
    DateTime EndDate
    );