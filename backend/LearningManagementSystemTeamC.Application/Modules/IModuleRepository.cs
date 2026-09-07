using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Application.Modules;

public interface IModuleRepository
{
    Task<Module?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Module>> GetModulesByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);
    Task AddAsync(Module module, CancellationToken cancellationToken);
}