namespace LearningManagementSystemTeamC.Application.Modules.Commands.CreateModule;

public record CreateModuleCommand(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    Guid CourseId);
