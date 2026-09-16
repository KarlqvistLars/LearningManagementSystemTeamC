using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.ReadModels;
using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class ActivityMapper
{
    public static ActivityDto ActivityToDto(Activity activity) =>
        new ActivityDto(
            activity.Id,
            activity.ActivityName,
            activity.Description,
            activity.StartDate,
            activity.EndDate,
            activity.Type,
            activity.ModuleId);

    public static ActivityDetailsDto ToDto(
        ActivityDetailsReadModel model)
    {
        return new ActivityDetailsDto(
            model.Id,
            model.ActivityName,
            model.Type,
            model.Description,
            model.StartDate,
            model.EndDate,
            model.ModuleId,
            model.ModuleName,
            model.CourseId,
            model.CourseName,
            model.SubmittedCount,
            model.TotalStudents);
    }
}
