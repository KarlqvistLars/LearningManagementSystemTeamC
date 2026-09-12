using LearningManagementSystemTeamC.Domain.Messages;

namespace LearningManagementSystemTeamC.Application.Messages;

public interface IMessageRepository
{
    Task AddAsync(
        Message message,
        CancellationToken cancellationToken);
}