using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Application.Modules;

public interface IModuleRepository
{

    Task<Module?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Module>> GetModulesByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);
}