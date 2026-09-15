using LearningManagementSystemTeamC.Application.Messages;
using LearningManagementSystemTeamC.Domain.Messages;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ApplicationDbContext _context;

    public MessageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Message message,
        CancellationToken cancellationToken)
    {
        await _context.Messages.AddAsync(
            message,
            cancellationToken);
    }
}