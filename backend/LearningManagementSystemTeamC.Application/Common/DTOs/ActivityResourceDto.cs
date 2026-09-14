using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record ActivityResourceDto(
    Guid Id,
    string ResourceName,
    string Content,
    string Url,
    DateTime CreatedAt,
    ActivityType ResourceType,
    Guid UserId,
    Guid ActivityId
);
