namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.DeleteChatRoom;

public interface IDeleteChatRoomHandler
{
    Task HandleAsync(
        DeleteChatRoomCommand command,
        CancellationToken cancellationToken);
}