using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Resources.Command.UpdateResource;

public interface IUpdateResourceHandler
{
    Task<ResourceDto> HandleAsync(
        UpdateResourceCommand command,
        Guid userId,
        string roleCode,
        CancellationToken cancellationToken);
}