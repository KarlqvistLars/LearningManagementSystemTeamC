using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Modules.Commands.EditModule;

public interface IEditModuleHandler
{
    Task<ModuleDto> Handle(EditModuleCommand command, CancellationToken cancellationToken);
}