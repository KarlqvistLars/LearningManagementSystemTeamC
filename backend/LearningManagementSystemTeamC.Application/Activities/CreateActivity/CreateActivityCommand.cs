using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Application.Activities.CreateActivity;

public record CreateActivityCommand(
    string ActivityName,
    ActivityType Type,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    Guid ModuleId
);
