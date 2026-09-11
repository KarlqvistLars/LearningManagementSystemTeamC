using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.ActivityResources.Command.CreateActivityResource;

public interface ICreateActivityResourceHandler
{
    Task<ActivityResourceDto> Handle(CreateActivityResourceCommand command, CancellationToken cancellationToken);
}
