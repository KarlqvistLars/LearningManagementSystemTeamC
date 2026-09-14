namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record ChatRoomMemberDto(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    DateTime JoinedAt,
    bool IsActive
);
