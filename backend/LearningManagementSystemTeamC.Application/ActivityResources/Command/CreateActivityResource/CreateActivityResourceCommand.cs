using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Application.ActivityResources.Command.CreateActivityResource;

public record CreateActivityResourceCommand(
    string ResourceName,
    string Content,
    string Url,
    DateTime CreatedAt,
    ActivityType Type,
    Guid UserId,
    Guid ActivityId
    );
