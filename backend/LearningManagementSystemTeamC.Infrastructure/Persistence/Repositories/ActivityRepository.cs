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

    public async Task<Activity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Activities
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> HasOverlappingAsync(
        Guid moduleId,
        DateTime startDate,
        DateTime endDate,
        Guid? excludeActivityId,
        CancellationToken cancellationToken)
    {
        return await _context.Activities
            .AnyAsync(
                x => x.ModuleId == moduleId
                    && (!excludeActivityId.HasValue || x.Id != excludeActivityId.Value)
                    && x.StartDate < endDate
                    && startDate < x.EndDate,
                cancellationToken);
    }
}