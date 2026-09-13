using LearningManagementSystemTeamC.Application.ChatRooms;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Domain.ChatRooms;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Application.Messages.Queries.GetMessages;

public class GetMessagesHandler : IGetMessagesHandler
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IMessageReadRepository _messageReadRepository;

    public GetMessagesHandler(
        IChatRoomRepository chatRoomRepository,
        IMessageReadRepository messageReadRepository)
    {
        _chatRoomRepository = chatRoomRepository;
        _messageReadRepository = messageReadRepository;
    }

    public async Task<IReadOnlyList<MessageDto>> HandleAsync(
        GetMessagesQuery query,
        Guid currentUserId,
        CancellationToken cancellationToken)
    {
        var chatRoom = await _chatRoomRepository.GetByIdAsync(
            query.ChatRoomId,
            cancellationToken)
            ?? throw new NotFoundException(
                ChatRoomRules.NotFoundCode,
                ChatRoomRules.NotFoundMessage);

        var isMember = chatRoom.Members
            .Any(member => member.UserId == currentUserId);

        if (!isMember)
            throw new UnauthorizedException(
                ChatRoomRules.NotMemberCode,
                ChatRoomRules.NotMemberMessage);

        var messages = await _messageReadRepository.GetByChatRoomIdAsync(
            query.ChatRoomId,
            cancellationToken);

        return messages
            .Select(MessageMapper.ToDto)
            .ToList();
    }
}