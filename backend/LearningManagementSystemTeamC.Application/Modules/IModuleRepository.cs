using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Application.Modules;

public interface IModuleRepository
{
    Task<IReadOnlyList<Module>> GetModulesByCourseIdAsync(Guid courseId);
}