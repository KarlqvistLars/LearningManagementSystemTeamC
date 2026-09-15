using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.Resources;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class ResourceMapper
{
    public static ResourceDto ResourceToDto(Resource resource) =>
        new ResourceDto(
            resource.Id,
            resource.ResourceName,
            resource.Content,
            resource.Url,
            resource.CreatedAt,
            resource.Type,
            resource.CreatedBy
        );
}
