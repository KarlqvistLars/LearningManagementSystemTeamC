using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class ActivityMapper
{
    public static ActivityDto ActivityToDto(Domain.Activities.Activity activity) =>
        new ActivityDto(
            activity.Id,
            activity.ActivityName,
            activity.Description,
            activity.StartDate,
            activity.EndDate,
            activity.Type,
            activity.ModuleId);
}
