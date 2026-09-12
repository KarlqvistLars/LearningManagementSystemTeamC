using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class ChatRoomReadRepository : IChatRoomReadRepository
{
    private readonly ApplicationDbContext _context;

    public ChatRoomReadRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ChatRoomReadModel?> GetByIdAsync(
    Guid chatRoomId,
    CancellationToken cancellationToken)
    {
        return await _context.ChatRooms
            .Where(chatRoom => chatRoom.Id == chatRoomId)
            .Select(chatRoom => new ChatRoomReadModel(
                chatRoom.Id,
                chatRoom.Name,
                chatRoom.CreatedAt,
                _context.ChatRoomMembers
                    .Where(member => member.ChatRoomId == chatRoom.Id)
                    .Join(
                        _context.Users,
                        member => member.UserId,
                        user => user.Id,
                        (member, user) => new
                        {
                            Member = member,
                            User = user
                        })
                    .Join(
                        _context.UserInfos,
                        x => x.User.Id,
                        userInfo => userInfo.UserId,
                        (x, userInfo) => new ChatRoomMemberReadModel(
                            x.User.Id,
                            userInfo.FirstName,
                            userInfo.LastName,
                            x.User.Email,
                            x.Member.JoinedAt,
                            x.User.IsActive))
                    .ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<ChatRoomReadModel>> GetByUserIdAsync(
    Guid userId,
    CancellationToken cancellationToken)
    {
        return await _context.ChatRooms
            .Where(chatRoom =>
                chatRoom.Members.Any(member => member.UserId == userId))
            .Select(chatRoom => new ChatRoomReadModel(
                chatRoom.Id,
                chatRoom.Name,
                chatRoom.CreatedAt,
                _context.ChatRoomMembers
                    .Where(member => member.ChatRoomId == chatRoom.Id)
                    .Join(
                        _context.Users,
                        member => member.UserId,
                        user => user.Id,
                        (member, user) => new
                        {
                            Member = member,
                            User = user
                        })
                    .Join(
                        _context.UserInfos,
                        x => x.User.Id,
                        userInfo => userInfo.UserId,
                        (x, userInfo) => new ChatRoomMemberReadModel(
                            x.User.Id,
                            userInfo.FirstName,
                            userInfo.LastName,
                            x.User.Email,
                            x.Member.JoinedAt,
                            x.User.IsActive))
                    .ToList()
            ))
            .ToListAsync(cancellationToken);
    }
}