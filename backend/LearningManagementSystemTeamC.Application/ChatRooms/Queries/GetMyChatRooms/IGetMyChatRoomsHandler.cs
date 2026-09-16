using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.ChatRooms.Queries.GetMyChatRooms;

public interface IGetMyChatRoomsHandler
{
    Task<IReadOnlyList<ChatRoomDto>> HandleAsync(
        GetMyChatRoomsQuery userId,
        CancellationToken cancellationToken);
}
