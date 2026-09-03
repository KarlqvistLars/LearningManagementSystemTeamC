using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record ActivityDto(
    Guid Id,
    string ActivityName,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    ActivityType Type,
    Guid ModuleId,
    string ModuleName
);