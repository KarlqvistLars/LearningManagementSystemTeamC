using LearningManagementSystemTeamC.Domain.Roles;

namespace LearningManagementSystemTeamC.Application.Roles;

public interface IRoleRepository
{
    Task<Role?> GetActiveById(Guid id);
}
