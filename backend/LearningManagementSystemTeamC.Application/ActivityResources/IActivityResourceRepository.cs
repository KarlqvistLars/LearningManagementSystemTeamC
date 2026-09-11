using LearningManagementSystemTeamC.Domain.ActivityResources;

namespace LearningManagementSystemTeamC.Application.ActivityResources;

public interface IActivityResourceRepository
{
    Task AddAsync(ActivityResource activityResource, CancellationToken cancellationToken);
    Task<IReadOnlyList<ActivityResource>> GetActivityResourcesByActivityIdAsync(
    Guid activityId,
    CancellationToken cancellationToken);

}
