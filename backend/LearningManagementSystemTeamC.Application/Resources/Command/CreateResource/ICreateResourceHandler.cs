using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Resources.Command.CreateResource;

public interface ICreateResourceHandler
{
    Task<ResourceDto> HandleAsync(CreateResourceCommand command, Guid userId, string roleCode, CancellationToken cancellationToken);
}
