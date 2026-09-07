using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Application.Modules.Commands.CreateModule;

public class CreateModuleHandler : ICreateModuleHandler
{
    private readonly IModuleRepository _moduleRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateModuleHandler(
        IModuleRepository moduleRepository,
        IUnitOfWork unitOfWork)
    {
        _moduleRepository = moduleRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ModuleDto> Handle(CreateModuleCommand command, CancellationToken cancellationToken)
    {
        var module = new Module(
            command.Name,
            command.Description,
            command.StartDate,
            command.EndDate,
            command.CourseId);

        await _moduleRepository.AddAsync(
            module,
            cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return new ModuleDto(module.Id, module.ModuleName, module.Description, module.StartDate, module.EndDate, module.CourseId);
       
    }
}