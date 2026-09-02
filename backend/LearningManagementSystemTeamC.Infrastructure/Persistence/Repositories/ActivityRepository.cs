using LearningManagementSystemTeamC.Domain.Activities;
using Microsoft.EntityFrameworkCore;
using LearningManagementSystemTeamC.Application.Activities;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class ActivityRepository : IActivityRepository
{
    private readonly ApplicationDbContext _context;

    public ActivityRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Activity>> GetActivitiesByModuleIdAsync(Guid moduleId)
    {
        return await _context.Activities
            .Include(x => x.Module)
            .Where(x => x.ModuleId == moduleId)
            .ToListAsync();
    }
}