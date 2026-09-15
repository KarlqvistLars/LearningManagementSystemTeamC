namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.AddChatRoomMember;

public record AddChatRoomMemberCommand(
    Guid ChatRoomId,
    Guid UserId);