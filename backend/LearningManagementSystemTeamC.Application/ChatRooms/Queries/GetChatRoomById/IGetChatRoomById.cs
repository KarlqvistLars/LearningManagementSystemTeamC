using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.ChatRooms.Queries.GetChatRoomById;

public interface IGetChatRoomByIdHandler
{
    Task<ChatRoomDto> HandleAsync(
        Guid chatRoomId,
        CancellationToken cancellationToken);
}