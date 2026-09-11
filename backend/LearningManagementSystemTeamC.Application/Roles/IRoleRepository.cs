using LearningManagementSystemTeamC.Domain.Roles;

namespace LearningManagementSystemTeamC.Application.Roles;

public interface IRoleRepository
{
    Task<Role?> GetActiveByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Role?> GetDefaultRoleAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Role>> GetAllActiveAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken);
}
