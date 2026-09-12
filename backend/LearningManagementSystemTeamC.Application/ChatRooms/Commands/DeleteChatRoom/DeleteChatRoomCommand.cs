namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.DeleteChatRoom;

public record DeleteChatRoomCommand(
    Guid ChatRoomId,
    Guid UserId);