using LearningManagementSystemTeamC.Domain.ActivityResources;
using LearningManagementSystemTeamC.Domain.Resources;

namespace LearningManagementSystemTeamC.Application.ActivityResources;

public interface IResourceRepository
{
    Task AddAsync(
        Resource resource,
        CancellationToken cancellationToken);

    Task AddActivityResourceAsync(
        ActivityResource activityResource,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Resource>> GetAllResourcesAsync(
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Resource>> GetResourcesByActivityIdAsync(
        Guid activityId,
        CancellationToken cancellationToken);

    Task<Resource?> GetResourceByIdAsync(
        Guid resourceId,
        CancellationToken cancellationToken);

    Task<Resource?> GetSubmissionByActivityAndUserIdAsync(
        Guid activityId,
        Guid userId,
        CancellationToken cancellationToken);
}