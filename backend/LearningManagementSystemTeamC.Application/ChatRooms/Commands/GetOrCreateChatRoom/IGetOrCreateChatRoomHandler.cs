using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.GetOrCreateChatRoom;

public interface IGetOrCreateChatRoomHandler
{
    Task<ChatRoomDto> HandleAsync(
        GetOrCreateChatRoomCommand command,
        Guid currentUserId,
        CancellationToken cancellationToken);
}