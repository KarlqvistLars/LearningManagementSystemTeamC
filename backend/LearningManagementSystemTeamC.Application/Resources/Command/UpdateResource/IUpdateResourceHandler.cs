using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Resources.Command.UpdateResource;

public interface IUpdateResourceHandler
{
    Task<ResourceDto?> Handle(
        UpdateResourceCommand command,
        CancellationToken cancellationToken);
}