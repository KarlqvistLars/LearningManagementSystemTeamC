using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivities;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;

namespace LearningManagementSystemTeamC.Application.Activities.Queries.GetAssignments;

public class GetAssignmentsHandler : IGetAssignmentsHandler
{
    private readonly IActivityReadRepository _activityReadRepository;

    public GetAssignmentsHandler(
        IActivityReadRepository activityReadRepository)
    {
        _activityReadRepository = activityReadRepository;
    }

    public async Task<IReadOnlyList<ActivityDetailsDto>> HandleAsync(
        GetAssignmentsQuery query,
        Guid userId,
        string roleCode,
        CancellationToken cancellationToken)
    {
        var assignments = await _activityReadRepository.GetAssignmentsAsync(
            userId,
            roleCode,
            cancellationToken);

        return assignments
            .Select(ActivityMapper.ToDto)
            .ToList();
    }
}