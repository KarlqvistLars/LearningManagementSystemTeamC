using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.Resources;
using LearningManagementSystemTeamC.Domain.UserInfos;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class ResourceMapper
{
    public static ResourceDto ResourceToDto(Resource resource) =>
        new ResourceDto(
            resource.Id,
            resource.ResourceName,
            resource.Content,
            resource.Url,
            resource.CreatedDate,
            resource.Type,
            resource.CreatedBy
        );

    public static ResourceWithCreatorDto ToResourceWithCreatorDto(
        Resource resource,
        UserInfo userInfo)
    {
        return new ResourceWithCreatorDto(
            resource.Id,
            resource.ResourceName,
            resource.Content,
            resource.Url,
            resource.CreatedDate,
            resource.Type,
            resource.CreatedBy,
            userInfo.FirstName,
            userInfo.LastName);
    }
}
