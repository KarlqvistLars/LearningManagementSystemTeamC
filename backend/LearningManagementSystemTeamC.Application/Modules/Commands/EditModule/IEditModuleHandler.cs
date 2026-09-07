using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModuleById;

namespace LearningManagementSystemTeamC.Application.Modules.Commands.EditModule;

public interface IEditModuleHandler
{
    Task<ModuleDto> Handle(EditModuleCommand command, CancellationToken cancellationToken);
}