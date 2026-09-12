using LearningManagementSystemTeamC.Application.ChatRooms;
using LearningManagementSystemTeamC.Domain.ChatRooms;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class ChatRoomRepository : IChatRoomRepository
{
    private readonly ApplicationDbContext _context;

    public ChatRoomRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        ChatRoom chatRoom,
        CancellationToken cancellationToken)
    {
        await _context.ChatRooms.AddAsync(
            chatRoom,
            cancellationToken);
    }

    public async Task<ChatRoom?> GetByIdAsync(
        Guid chatRoomId,
        CancellationToken cancellationToken)
    {
        return await _context.ChatRooms
            .Include(chatRoom => chatRoom.Members)
            .FirstOrDefaultAsync(
                chatRoom => chatRoom.Id == chatRoomId,
                cancellationToken);
    }

    public async Task<bool> ExistsWithMembersAsync(
        IReadOnlyCollection<Guid> userIds,
        CancellationToken cancellationToken)
    {
        var memberCount = userIds.Count;

        return await _context.ChatRooms
            .Where(chatRoom =>
                chatRoom.Members.Count == memberCount &&
                chatRoom.Members.All(
                    member => userIds.Contains(member.UserId)))
            .AnyAsync(cancellationToken);
    }

    public void Remove(
        ChatRoom chatRoom)
    {
        _context.ChatRooms.Remove(chatRoom);
    }
}