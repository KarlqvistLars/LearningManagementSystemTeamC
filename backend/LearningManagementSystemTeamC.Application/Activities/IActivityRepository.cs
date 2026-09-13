namespace LearningManagementSystemTeamC.Application.Activities;

public interface IActivityRepository
{
    Task AddAsync(Domain.Activities.Activity activity, CancellationToken cancellationToken);
    Task<IReadOnlyList<Domain.Activities.Activity>> GetActivitiesByModuleIdAsync(Guid moduleId, CancellationToken cancellationToken);
}