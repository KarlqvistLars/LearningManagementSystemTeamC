using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Courses;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Application.Modules.Queries.GetModule;

public class GetModuleHandler : IGetModuleHandler
{
    private readonly IModuleRepository _moduleRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetModuleHandler(
        IModuleRepository moduleRepository,
        ICourseRepository courseRepository,
        IUnitOfWork unitOfWork)
    {
        _moduleRepository = moduleRepository;
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ModuleDto>> Handle(
        GetModuleQuery query, CancellationToken cancellationToken)
    {
        var checkIfCourseExist = await _courseRepository.GetByIdAsync(query.CourseId, cancellationToken)
        ?? throw new DomainException(ModuleRules.InvalidCourseIdCode, ModuleRules.InvalidCourseIdMessage);

        var modules = await _moduleRepository.GetModulesByCourseIdAsync(query.CourseId, cancellationToken);

        return modules.Select(module => ModuleMapper.ModuleToDto(module)).ToList();
    }
}