using LearningManagementSystemTeamC.Application.ActivityResources;
using LearningManagementSystemTeamC.Domain.ActivityResources;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class ActivityResourceRepository : IActivityResourceRepository
{
    private readonly ApplicationDbContext _context;
    public ActivityResourceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ActivityResource activityResource, CancellationToken cancellationToken)
    {
        await _context.ActivityResources.AddAsync(activityResource, cancellationToken);
    }

    public async Task<IReadOnlyList<ActivityResource>>
        GetActivityResourcesByActivityIdAsync(
            Guid activityId,
            CancellationToken cancellationToken)
    {
        return await _context.ActivityResources
            .Where(resource => resource.ActivityId == activityId)
            .ToListAsync(cancellationToken);
    }
}
