using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Application.Activities;

public interface IActivityRepository
{
    Task AddAsync(Activity activity, CancellationToken cancellationToken);
    Task<IReadOnlyList<Activity>> GetActivitiesByModuleIdAsync(Guid moduleId);
}