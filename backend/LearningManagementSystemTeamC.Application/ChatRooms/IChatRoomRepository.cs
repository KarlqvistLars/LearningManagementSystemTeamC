using LearningManagementSystemTeamC.Domain.ChatRooms;

namespace LearningManagementSystemTeamC.Application.ChatRooms;

public interface IChatRoomRepository
{
    Task AddAsync(
        ChatRoom chatRoom,
        CancellationToken cancellationToken);
    Task<ChatRoom?> GetByIdAsync(
        Guid chatRoomId,
        CancellationToken cancellationToken);

    void Remove(
        ChatRoom chatRoom);

    Task<bool> ExistsWithMembersAsync(
        IReadOnlyCollection<Guid> userIds,
        CancellationToken cancellationToken);
}
