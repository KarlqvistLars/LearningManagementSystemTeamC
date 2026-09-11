using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModuleById;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Application.Modules.Commands.EditModule;

public class EditModuleHandler : IEditModuleHandler
{
    private readonly IModuleRepository _moduleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditModuleHandler(
        IModuleRepository moduleRepository,
        IUnitOfWork unitOfWork)
    {
        _moduleRepository = moduleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ModuleDto> Handle(EditModuleCommand command, CancellationToken cancellationToken)
    {
        var module = await _moduleRepository.GetByIdAsync(command.Id, cancellationToken);

        if (module is null)
        {
            throw new NotFoundException(ModuleRules.ModuleNotFoundCode, ModuleRules.ModuleNotFoundMessage);
        }

        module.Update(
            command.Name,
            command.Description,
            command.StartDate,
            command.EndDate,
            command.CourseId);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ModuleMapper.ModuleToDto(module);
    }
}