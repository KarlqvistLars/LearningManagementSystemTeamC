using LearningManagementSystemTeamC.Application.Modules;
using LearningManagementSystemTeamC.Domain.Modules;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class ModuleRepository : IModuleRepository
{
    private readonly ApplicationDbContext _context;

    public ModuleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Module>> GetModulesByCourseIdAsync(Guid courseId)
    {
        return await _context.Modules
            .Include(x => x.Course)
            .Where(x => x.CourseId == courseId)
            .ToListAsync();
    }
}