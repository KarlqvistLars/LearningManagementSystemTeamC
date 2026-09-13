namespace LearningManagementSystemTeamC.Application.Common.ReadModels;

public record MessageReadModel(
    Guid Id,
    Guid ChatRoomId,
    Guid SenderId,
    string FirstName,
    string LastName,
    string Content,
    DateTime CreatedAt
);