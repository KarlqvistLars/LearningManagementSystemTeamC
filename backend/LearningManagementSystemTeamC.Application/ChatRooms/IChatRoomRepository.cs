using LearningManagementSystemTeamC.Domain.ChatRooms;

namespace LearningManagementSystemTeamC.Application.ChatRooms;

public interface IChatRoomRepository
{
    Task AddAsync(
        ChatRoom chatRoom,
        CancellationToken cancellationToken);
}
