using LearningManagementSystemTeamC.Application.ActivityResources;
using LearningManagementSystemTeamC.Domain.Resources;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly ApplicationDbContext _context;
    public ResourceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Resource resource, CancellationToken cancellationToken)
    {
        await _context.Resources.AddAsync(resource, cancellationToken);
    }

    public async Task<IReadOnlyList<Resource>>
        GetResourcesByActivityIdAsync(
            Guid activityId,
            CancellationToken cancellationToken)
    {
        return await _context.Resources
            .Where(resource => resource.Id == activityId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Resource>> GetAllResourcesAsync(
        Guid resourcesId,
        CancellationToken cancellationToken)
    {
        return await _context.Resources
            .ToListAsync(cancellationToken);
    }
}
