using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.CreateChatRoom;

public interface ICreateChatRoomHandler
{
    Task<ChatRoomDto> HandleAsync(
        CreateChatRoomCommand command,
        Guid userId,
        CancellationToken cancellationToken);
}