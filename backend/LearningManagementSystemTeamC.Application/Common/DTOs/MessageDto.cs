namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record MessageDto(
    Guid Id,
    Guid SenderId,
    string? SenderFirstName,
    string? SenderLastName,
    string Content,
    DateTime CreatedAt
);