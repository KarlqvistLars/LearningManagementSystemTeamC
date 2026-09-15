namespace LearningManagementSystemTeamC.Application.Common.ReadModels;

public record ChatRoomMemberReadModel(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    DateTime JoinedAt,
    bool IsActive
);