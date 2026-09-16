using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Activities.Queries.GetAssignmentSubmissions;

public interface IGetAssignmentSubmissionsHandler
{
    Task<IReadOnlyList<AssignmentSubmissionDto>> HandleAsync(
        GetAssignmentSubmissionsQuery query,
        CancellationToken cancellationToken);
}