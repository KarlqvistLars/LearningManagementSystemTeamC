using LearningManagementSystemTeamC.Application.Common.ReadModels;

namespace LearningManagementSystemTeamC.Application.Common.Interfaces;

public interface IActivityReadRepository
{
    Task<List<ActivityDetailsReadModel>> GetAssignmentsAsync(
        Guid userId,
        string roleCode,
        CancellationToken cancellationToken);

    Task<List<AssignmentSubmissionReadModel>> GetAssignmentSubmissionsAsync(
        Guid activityId,
        CancellationToken cancellationToken);
}