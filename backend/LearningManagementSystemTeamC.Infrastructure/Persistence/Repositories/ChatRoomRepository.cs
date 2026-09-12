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

    public void Remove(
        ChatRoom chatRoom)
    {
        _context.ChatRooms.Remove(chatRoom);
    }
}