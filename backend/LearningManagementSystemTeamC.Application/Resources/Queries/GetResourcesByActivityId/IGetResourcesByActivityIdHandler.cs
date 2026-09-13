using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.ActivityResources.Queries.GetResourcesByActivityId;

public interface IGetResourcesByActivityIdHandler
{
    Task<IReadOnlyList<ResourceDto>> Handle(
        GetResourcesByActivityIdQuery query,
        CancellationToken cancellationToken);
}
