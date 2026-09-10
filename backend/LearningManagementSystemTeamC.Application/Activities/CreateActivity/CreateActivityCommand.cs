using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Application.Activities.CreateActivity;

public record CreateActivityCommand(
    string ActivityName,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    ActivityType Type,
    Guid ModuleId
);
