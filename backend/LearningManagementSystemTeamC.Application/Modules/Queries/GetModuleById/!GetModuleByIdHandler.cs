using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Modules.Queries.GetModuleById;

public interface IGetModuleByIdHandler
{
    Task<ModuleDto?> Handle(GetModuleByIdQuery query, CancellationToken cancellationToken);
}