using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Application.Modules;

public interface IModuleRepository
{
    Task<IReadOnlyList<Module>> GetModulesByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);

    Task AddAsync(Module module, CancellationToken cancellationToken);
}