using LearningManagementSystemTeamC.Application.Common.ReadModels;

namespace LearningManagementSystemTeamC.Application.Common.Interfaces;

public interface IChatRoomReadRepository
{
    Task<ChatRoomReadModel?> GetByIdAsync(
        Guid chatRoomId,
        CancellationToken cancellationToken);

    Task<List<ChatRoomReadModel>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken);
}