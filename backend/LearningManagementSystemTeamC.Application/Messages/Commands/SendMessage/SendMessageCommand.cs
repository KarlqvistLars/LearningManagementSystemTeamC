namespace LearningManagementSystemTeamC.Application.Messages.Commands.SendMessage;

public record SendMessageCommand(
    Guid ChatRoomId,
    string Content);