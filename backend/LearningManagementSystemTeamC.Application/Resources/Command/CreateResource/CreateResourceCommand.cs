using LearningManagementSystemTeamC.Domain.Resources;

namespace LearningManagementSystemTeamC.Application.Resources.Command.CreateResource;

public record CreateResourceCommand(
    string ResourceName,
    string Content,
    string Url,
    DateTime CreatedDate,
    ResourceType Type
    );
