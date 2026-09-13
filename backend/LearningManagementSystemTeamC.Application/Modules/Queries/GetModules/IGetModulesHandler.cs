using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Modules.Queries.GetModules;

public interface IGetModulesHandler
{
    Task<IReadOnlyList<ModuleDto>> Handle(GetModulesQuery query, CancellationToken cancellationToken);
}