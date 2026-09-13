using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class MessageReadRepository : IMessageReadRepository
{
    private readonly ApplicationDbContext _context;

    public MessageReadRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MessageReadModel>> GetByChatRoomIdAsync(
        Guid chatRoomId,
        CancellationToken cancellationToken)
    {
        return await _context.Messages
                .Where(message => message.ChatRoomId == chatRoomId)
                .Join(_context.Users,
                      message => message.SenderId,
                      sender => sender.Id,
                      (message, sender) => new
                      {
                          Message = message,
                          Sender = sender
                      })
                .Join(_context.UserInfos,
                    x => x.Sender.Id,
                    u => u.UserId,
                    (x, u) => new MessageReadModel(
                        x.Message.Id,
                        x.Message.ChatRoomId,
                        x.Sender.Id,
                        u.FirstName,
                        u.LastName,
                        x.Message.Content,
                        x.Message.CreatedAt
                        ))
                .OrderBy(message => message.CreatedAt)
                .ToListAsync(cancellationToken);
    }
}