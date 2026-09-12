using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Domain.ChatRooms;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Application.ChatRooms.Queries.GetChatRoomById;

public class GetChatRoomByIdHandler : IGetChatRoomByIdHandler
{
    private readonly IChatRoomReadRepository _chatRoomReadRepository;

    public GetChatRoomByIdHandler(
        IChatRoomReadRepository chatRoomReadRepository)
    {
        _chatRoomReadRepository = chatRoomReadRepository;
    }

    public async Task<ChatRoomDto> HandleAsync(
        GetChatRoomByIdQuery query,
        CancellationToken cancellationToken)
    {
        var chatRoomReadModel =
            await _chatRoomReadRepository.GetByIdAsync(
                query.Id,
                cancellationToken);

        return chatRoomReadModel is null
            ? throw new NotFoundException(
                ChatRoomRules.NotFoundCode,
                ChatRoomRules.NotFoundMessage)
            : ChatRoomMapper.ToDto(chatRoomReadModel);
    }
}