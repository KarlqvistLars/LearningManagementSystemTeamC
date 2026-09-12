namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record MessageDto(
    Guid Id,
    Guid ChatRoomId,
    Guid SenderId,
    string Content,
    DateTime CreatedAt
);