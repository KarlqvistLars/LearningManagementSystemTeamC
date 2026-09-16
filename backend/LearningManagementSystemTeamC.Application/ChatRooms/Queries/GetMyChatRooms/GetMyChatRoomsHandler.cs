using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;

namespace LearningManagementSystemTeamC.Application.ChatRooms.Queries.GetMyChatRooms;

public class GetMyChatRoomsHandler : IGetMyChatRoomsHandler
{
    private readonly IChatRoomReadRepository _chatRoomReadRepository;

    public GetMyChatRoomsHandler(
        IChatRoomReadRepository chatRoomReadRepository)
    {
        _chatRoomReadRepository = chatRoomReadRepository;
    }

    public async Task<IReadOnlyList<ChatRoomDto>> HandleAsync(
        GetMyChatRoomsQuery query,
        CancellationToken cancellationToken)
    {
        var chatRooms = await _chatRoomReadRepository.GetByUserIdAsync(
                query.UserId,
                cancellationToken);

        return chatRooms
            .Select(ChatRoomMapper.ToDto)
            .ToList();
    }
}