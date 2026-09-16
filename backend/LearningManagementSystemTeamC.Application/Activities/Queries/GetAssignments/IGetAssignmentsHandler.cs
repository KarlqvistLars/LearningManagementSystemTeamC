using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivities;
using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Activities.Queries.GetAssignments;

public interface IGetAssignmentsHandler
{
    Task<IReadOnlyList<ActivityDetailsDto>> HandleAsync(
        GetAssignmentsQuery query,
        Guid userId,
        string roleCode,
        CancellationToken cancellationToken);
}