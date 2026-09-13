namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.AddChatRoomMember;

public interface IAddChatRoomMemberHandler
{
    Task HandleAsync(
        AddChatRoomMemberCommand command,
        Guid currentUserId,
        CancellationToken cancellationToken);
}