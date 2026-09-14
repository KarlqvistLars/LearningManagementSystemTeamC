using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Resources.Queries.GetAllResources;

public interface IGetAllResourcesHandler
{
    Task<IReadOnlyList<ResourceDto>> Handle(
        GetAllResourcesQuery query,
        CancellationToken cancellationToken);
}
