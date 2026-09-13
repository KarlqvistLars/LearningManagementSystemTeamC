using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.ReadModels;
using LearningManagementSystemTeamC.Domain.Messages;
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
        return await BuildQuery(
                _context.Messages.Where(
                    message => message.ChatRoomId == chatRoomId)
                    .OrderBy(message => message.CreatedAt))
            .ToListAsync(cancellationToken);
    }

    public async Task<MessageReadModel?> GetByIdAsync(
        Guid messageId,
        CancellationToken cancellationToken)
    {
        return await BuildQuery(
                _context.Messages.Where(
                    message => message.Id == messageId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    private IQueryable<MessageReadModel> BuildQuery(
        IQueryable<Message> messages)
    {
        return messages
            .Join(
                _context.Users,
                message => message.SenderId,
                sender => sender.Id,
                (message, sender) => new
                {
                    Message = message,
                    Sender = sender
                })
            .Join(
                _context.UserInfos,
                x => x.Sender.Id,
                userInfo => userInfo.UserId,
                (x, userInfo) => new MessageReadModel(
                    x.Message.Id,
                    x.Message.ChatRoomId,
                    x.Sender.Id,
                    userInfo.FirstName,
                    userInfo.LastName,
                    x.Message.Content,
                    x.Message.CreatedAt));
    }
}