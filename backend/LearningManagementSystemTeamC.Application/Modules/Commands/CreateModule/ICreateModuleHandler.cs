using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Modules.Commands.CreateModule;

public interface ICreateModuleHandler
{
    Task<ModuleDto> Handle(CreateModuleCommand command, CancellationToken cancellationToken);
}