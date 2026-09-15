using LearningManagementSystemTeamC.Domain.Resources;

namespace LearningManagementSystemTeamC.Application.Resources.Command.UpdateResource;

public record UpdateResourceCommand(
    Guid ResourceId,
    string ResourceName,
    string Content,
    string? Url,
    ResourceType Type);