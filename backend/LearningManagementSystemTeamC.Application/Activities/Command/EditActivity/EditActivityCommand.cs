using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Application.Activities.Command.EditActivity;

public record EditActivityCommand(
    Guid Id,
    string ActivityName,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    ActivityType Type,
    Guid ModuleId);
