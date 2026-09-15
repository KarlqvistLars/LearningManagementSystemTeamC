using LearningManagementSystemTeamC.Domain.Resources;

namespace LearningManagementSystemTeamC.Application.ActivityResources;

public interface IResourceRepository
{
    Task AddAsync(Resource resource, CancellationToken cancellationToken);
    Task<IReadOnlyList<Resource>> GetAllResourcesAsync(
        CancellationToken cancellationToken);
    Task<IReadOnlyList<Resource>> GetResourcesByActivityIdAsync(
        Guid activityId,
        CancellationToken cancellationToken);
    Task<Resource?> GetResourceByIdAsync(Guid resourceId, CancellationToken cancellationToken);
}
