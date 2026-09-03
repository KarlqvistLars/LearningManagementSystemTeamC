using LearningManagementSystemTeamC.Application.Roles;
using LearningManagementSystemTeamC.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetActiveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Roles.FirstOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);
    }

    public async Task<Role?> GetDefaultRoleAsync(CancellationToken cancellationToken)
    {
        return await _context.Roles.FirstOrDefaultAsync(x => x.Code == RoleRules.DefaultRoleCode && x.IsActive, cancellationToken);
    }
}
