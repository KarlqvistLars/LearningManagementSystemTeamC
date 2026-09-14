using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.ActivityResources.Queries.GetResourcesByActivityId;

public interface IGetResourcesByActivityIdHandler
{
    Task<IReadOnlyList<ActivityResourceDto>> Handle(
        GetResourcesByActivityIdQuery query,
        CancellationToken cancellationToken);
}
