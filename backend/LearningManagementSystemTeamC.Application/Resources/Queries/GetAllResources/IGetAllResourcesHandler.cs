using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Resources.Queries.GetAllResources;

public interface IGetAllResourcesHandler
{
    Task<IReadOnlyList<ResourceWithCreatorDto>> HandleAsync(
        GetAllResourcesQuery query,
        CancellationToken cancellationToken);
}
