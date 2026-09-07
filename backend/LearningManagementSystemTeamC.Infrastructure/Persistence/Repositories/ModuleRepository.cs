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

    public async Task<Module?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Modules
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Module>> GetModulesByCourseIdAsync(Guid courseId, CancellationToken cancellationToken)
    {
        return await _context.Modules
            .Where(x => x.CourseId == courseId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Module module, CancellationToken cancellationToken)
    {
        await _context.Modules.AddAsync(module, cancellationToken);
    }


}