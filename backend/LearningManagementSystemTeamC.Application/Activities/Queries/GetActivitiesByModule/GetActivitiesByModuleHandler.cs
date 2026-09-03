using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;

namespace LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModule;

public class GetActivitiesByModuleHandler : IGetActivitiesByModuleHandler
{
    private readonly IActivityRepository _activityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetActivitiesByModuleHandler(
        IActivityRepository activityRepository,
        IUnitOfWork unitOfWork)
    {
        _activityRepository = activityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ActivityDto>> Handle(
        GetActivitiesByModuleQuery query,
        CancellationToken cancellationToken)
    {
        var activities = await _activityRepository.GetActivitiesByModuleIdAsync(query.ModuleId);

        return activities.Select(activity => new ActivityDto(
            activity.Id,
            activity.ActivityName,
            activity.Description,
            activity.StartDate,
            activity.EndDate,
            activity.Type,
            activity.ModuleId,
            activity.Module.ModuleName))
            .ToList();
    }
}