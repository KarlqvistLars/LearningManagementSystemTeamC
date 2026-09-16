using LearningManagementSystemTeamC.Application.Common.ReadModels;

namespace LearningManagementSystemTeamC.Application.Common.Interfaces;

public interface IMessageReadRepository
{
    Task<List<MessageReadModel>> GetByChatRoomIdAsync(
        Guid chatRoomId,
        CancellationToken cancellationToken);

    Task<MessageReadModel?> GetByIdAsync(
        Guid messageId,
        CancellationToken cancellationToken);
}