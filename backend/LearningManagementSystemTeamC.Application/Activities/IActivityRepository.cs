using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Application.Activities;

public interface IActivityRepository
{
    Task AddAsync(Activity activity, CancellationToken cancellationToken);
    Task<IReadOnlyList<Activity>> GetActivitiesByModuleIdAsync(Guid moduleId, CancellationToken cancellationToken);
    Task<Activity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> HasOverlappingAsync(
        Guid moduleId,
        DateTime startDate,
        DateTime endDate,
        Guid? excludeActivityId,
        CancellationToken cancellationToken);
}