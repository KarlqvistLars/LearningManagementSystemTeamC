using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Modules.Queries.GetModule;

public interface IGetModuleHandler
{
    Task<IReadOnlyList<ModuleDto>> Handle(GetModuleQuery query, CancellationToken cancellationToken);
}