namespace LearningManagementSystemTeamC.Application.Common.ReadModels;

public record ChatRoomReadModel(
    Guid Id,
    string? Name,
    DateTime CreatedAt,
    List<ChatRoomMemberReadModel> Members
);