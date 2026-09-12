namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.CreateChatRoom;

public interface ICreateChatRoomHandler
{
    Task<Guid> HandleAsync(
        CreateChatRoomCommand command,
        Guid creatorId,
        CancellationToken cancellationToken);
}