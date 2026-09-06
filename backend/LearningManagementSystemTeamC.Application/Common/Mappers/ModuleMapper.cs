using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class ModuleMapper
{
    public static ModuleDto ModuleToDto(Module module) =>
        new ModuleDto(
            module.Id,
            module.ModuleName,
            module.Description,
            module.StartDate,
            module.EndDate,
            module.CourseId
        );
}