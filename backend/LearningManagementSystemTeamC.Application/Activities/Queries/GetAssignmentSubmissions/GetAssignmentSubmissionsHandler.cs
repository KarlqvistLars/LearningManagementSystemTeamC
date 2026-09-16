using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;

namespace LearningManagementSystemTeamC.Application.Activities.Queries.GetAssignmentSubmissions;

public class GetAssignmentSubmissionsHandler
    : IGetAssignmentSubmissionsHandler
{
    private readonly IActivityReadRepository _activityReadRepository;

    public GetAssignmentSubmissionsHandler(
        IActivityReadRepository activityReadRepository)
    {
        _activityReadRepository = activityReadRepository;
    }

    public async Task<IReadOnlyList<AssignmentSubmissionDto>> HandleAsync(
        GetAssignmentSubmissionsQuery query,
        CancellationToken cancellationToken)
    {
        var readModels =
            await _activityReadRepository.GetAssignmentSubmissionsAsync(
                query.ActivityId,
                cancellationToken);

        return readModels
            .Select(AssignmentSubmissionMapper.Map)
            .ToList();
    }
}