using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Resources.Command.CreateResource;

namespace LearningManagementSystemTeamC.Application.ActivityResources.Command.CreateActivityResource;

public interface ICreateResourceHandler
{
    Task<ResourceDto> Handle(CreateResourceCommand command, CancellationToken cancellationToken);
}
