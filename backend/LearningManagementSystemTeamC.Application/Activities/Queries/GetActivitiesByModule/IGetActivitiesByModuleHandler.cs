using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModule;

public interface IGetActivitiesByModuleHandler
{
    Task<IReadOnlyList<ActivityDto>> Handle(
        GetActivitiesByModuleQuery query,
        CancellationToken cancellationToken);
}
