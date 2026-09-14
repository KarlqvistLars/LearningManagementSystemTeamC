using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record ActivityDto(
    Guid Id,
    string ActivityName,
    ActivityType Type,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    Guid ModuleId
);