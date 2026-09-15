namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.RemoveChatRoomMember;

public record RemoveChatRoomMemberCommand(
    Guid ChatRoomId,
    Guid TargetUserId);