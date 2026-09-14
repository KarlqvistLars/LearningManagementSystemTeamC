using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class ActivityMapper
{
    public static ActivityDto ActivityToDto(Activity activity) =>
        new ActivityDto(
            activity.Id,
            activity.ActivityName,
            activity.Type,
            activity.Description,
            activity.StartDate,
            activity.EndDate,
            activity.ModuleId);
}
