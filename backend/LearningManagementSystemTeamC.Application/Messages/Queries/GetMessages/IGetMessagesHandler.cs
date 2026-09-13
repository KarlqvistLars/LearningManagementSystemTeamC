using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Messages.Queries.GetMessages;

public interface IGetMessagesHandler
{
    Task<IReadOnlyList<MessageDto>> HandleAsync(
        GetMessagesQuery query,
        Guid currentUserId,
        CancellationToken cancellationToken);
}