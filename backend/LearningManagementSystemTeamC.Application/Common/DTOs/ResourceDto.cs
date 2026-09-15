using LearningManagementSystemTeamC.Domain.Resources;

namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record ResourceDto(
    Guid Id,
    string ResourceName,
    string Content,
    string? Url,
    DateTime CreatedDate,
    ResourceType Type,
    Guid CreatedBy
);

public record ResourceWithCreatorDto(
    Guid Id,
    string ResourceName,
    string Content,
    string? Url,
    DateTime CreatedDate,
    ResourceType Type,
    Guid CreatedBy,
    string CreatedByFirstName,
    string CreatedByLastName
);