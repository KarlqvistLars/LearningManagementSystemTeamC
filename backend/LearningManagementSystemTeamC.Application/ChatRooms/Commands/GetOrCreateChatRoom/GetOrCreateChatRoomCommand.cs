namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.GetOrCreateChatRoom;

public record GetOrCreateChatRoomCommand(
    Guid TargetUserId);