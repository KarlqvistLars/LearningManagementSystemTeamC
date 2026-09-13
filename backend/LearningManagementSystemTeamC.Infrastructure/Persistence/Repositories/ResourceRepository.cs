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
        var resourceIds = await _context.ActivityResources
       .Where(ar => ar.ActivityId == activityId)
       .Select(ar => ar.ResourceId)
       .ToListAsync(cancellationToken);

        Console.WriteLine($"Resource IDs for activityId {activityId}: {string.Join(", ", resourceIds)}");

        return await _context.Resources
            .Where(resource => resourceIds.Contains(resource.Id))
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
