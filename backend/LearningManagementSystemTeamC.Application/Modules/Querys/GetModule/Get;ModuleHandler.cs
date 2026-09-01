using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;

namespace LearningManagementSystemTeamC.Application.Modules.Query.GetModule;

public class GetModuleHandler
{
    private readonly IModuleRepository _moduleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetModuleHandler(
        IModuleRepository moduleRepository,
        IUnitOfWork unitOfWork)
    {
        _moduleRepository = moduleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ModuleDto>> Handle(
        GetModuleQuery query)
    {
        var modules = await _moduleRepository.GetModulesByCourseIdAsync(query.CourseId);

        return modules.Select(module => new ModuleDto(
            module.Id,
            module.ModuleName,
            module.Description,
            module.StartDate,
            module.EndDate,
            module.CourseId,
            module.Course.CourseName))
            .ToList();
    }
}