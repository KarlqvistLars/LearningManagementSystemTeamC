using LearningManagementSystemTeamC.Application.ChatRooms;
using LearningManagementSystemTeamC.Domain.ChatRooms;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class ChatRoomRepository : IChatRoomRepository
{
    private readonly ApplicationDbContext _context;

    public ChatRoomRepository(ApplicationDbContext context)
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
}
