using LearningManagementSystemTeamC.Application.ActivityResources;
using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Resources.Queries.GetMySubmission;

public class GetMySubmissionHandler : IGetMySubmissionHandler
{
    private readonly IResourceRepository _resourceRepository;

    public GetMySubmissionHandler(
        IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }

    public async Task<ResourceDto?> HandleAsync(
        GetMySubmissionQuery query,
        CancellationToken cancellationToken)
    {
        var resource =
            await _resourceRepository
                .GetSubmissionByActivityAndUserIdAsync(
                    query.ActivityId,
                    query.UserId,
                    cancellationToken);

        if (resource is null)
            return null;

        return new ResourceDto(
            resource.Id,
            resource.ResourceName,
            resource.Content,
            resource.Url,
            resource.CreatedAt,
            resource.Type,
            resource.CreatedBy);
    }
}