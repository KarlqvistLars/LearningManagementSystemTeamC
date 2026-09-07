using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Application.Activities;

public interface IActivityRepository
{
    Task<IReadOnlyList<Activity>> GetActivitiesByModuleIdAsync(Guid moduleId);
}