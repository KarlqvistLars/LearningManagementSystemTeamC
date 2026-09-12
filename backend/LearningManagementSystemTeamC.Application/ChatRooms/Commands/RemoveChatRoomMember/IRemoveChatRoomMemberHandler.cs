namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.RemoveChatRoomMember;

public interface IRemoveChatRoomMemberHandler
{
    Task HandleAsync(
        RemoveChatRoomMemberCommand command,
        Guid currentUserId,
        CancellationToken cancellationToken);
}