using LearningManagementSystemTeamC.Application.Activities;
using LearningManagementSystemTeamC.Domain.Activities;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class ActivityRepository : IActivityRepository
{
    private readonly ApplicationDbContext _context;

    public ActivityRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Activity>> GetActivitiesByModuleIdAsync(Guid moduleId, CancellationToken cancellationToken)
    {
        return await _context.Activities
            .Where(x => x.ModuleId == moduleId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Activity activity, CancellationToken cancellationToken)
    {
        await _context.Activities.AddAsync(activity, cancellationToken);
    }
}