using LearningManagementSystemTeamC.Domain.Resources;

namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record ResourceDto(
    Guid Id,
    string ResourceName,
    string Content,
    string Url,
    DateTime CreatedAt,
    ResourceType Type
);

public record CreateResourceDto(
    string ResourceName,
    string Content,
    string Url,
    DateTime CreatedAt,
    ResourceType Type
);