using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.ActivityResources.Queries.GetResourcesByActivityId;

public interface IGetResourcesByActivityIdHandler
{
    Task<IReadOnlyList<ResourceWithCreatorDto>> HandleAsync(
        GetResourcesByActivityIdQuery query,
        CancellationToken cancellationToken);
}
