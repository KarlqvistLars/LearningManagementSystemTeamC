namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record ChatRoomDto(
    Guid Id,
    string? Name,
    DateTime CreatedAt,
    List<ChatRoomMemberDto> Members
);