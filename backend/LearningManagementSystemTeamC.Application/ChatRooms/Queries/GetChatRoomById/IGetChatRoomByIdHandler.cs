using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.ChatRooms.Queries.GetChatRoomById;

public interface IGetChatRoomByIdHandler
{
    Task<ChatRoomDto> HandleAsync(
        GetChatRoomByIdQuery Id,
        CancellationToken cancellationToken);
}