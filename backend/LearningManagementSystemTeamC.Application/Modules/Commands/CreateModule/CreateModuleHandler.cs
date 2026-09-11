using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Courses;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Courses;
using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Application.Modules.Commands.CreateModule;

public class CreateModuleHandler : ICreateModuleHandler
{
    private readonly IModuleRepository _moduleRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateModuleHandler(
        IModuleRepository moduleRepository,
        ICourseRepository courseRepository,
        IUnitOfWork unitOfWork)
    {
        _moduleRepository = moduleRepository;
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ModuleDto> Handle(CreateModuleCommand command, CancellationToken cancellationToken)
    {
        var courseId = await _courseRepository.GetByIdAsync(command.CourseId, cancellationToken);

        if (courseId == null)
        {
            throw new NotFoundException(CourseRules.CourseNotFoundCode, CourseRules.CourseNotFoundMessage);
        }

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

        return ModuleMapper.ModuleToDto(module);
       
    }
}