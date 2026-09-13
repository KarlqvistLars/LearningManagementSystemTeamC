namespace LearningManagementSystemTeamC.Application.Modules.Commands.EditModule;

public record EditModuleCommand(
    Guid Id,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    Guid CourseId);