using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModuleId;

public interface IGetActivitiesByModuleIdHandler
{
    Task<IReadOnlyList<ActivityDto>> Handle(
        GetActivitiesByModuleIdQuery query,
        CancellationToken cancellationToken);
}
