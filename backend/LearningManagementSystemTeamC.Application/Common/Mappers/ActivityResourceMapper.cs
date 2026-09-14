using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.ActivityResources;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class ActivityResourceMapper
{
    public static ActivityResourceDto ActivityResourceToDto(ActivityResource activityResource) =>
        new ActivityResourceDto(
            activityResource.Id,
            activityResource.ResourceName,
            activityResource.Content,
            activityResource.Url,
            activityResource.CreatedAt,
            activityResource.ResourceType,
            activityResource.UserId,
            activityResource.ActivityId
        );
}
