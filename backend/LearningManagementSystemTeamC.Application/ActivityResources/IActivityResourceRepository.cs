using LearningManagementSystemTeamC.Domain.ActivityResources;

namespace LearningManagementSystemTeamC.Application.ActivityResources
{
    public interface IActivityResourceRepository
    {
        Task<IReadOnlyList<ActivityResource>> GetActivityResourcesByActivityIdAsync(
     Guid activityId,
     CancellationToken cancellationToken);
    }
}
