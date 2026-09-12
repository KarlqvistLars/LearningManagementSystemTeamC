using LearningManagementSystemTeamC.Application.ChatRooms;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Domain.ChatRooms;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Messages;

namespace LearningManagementSystemTeamC.Application.Messages.Commands.SendMessage;

public class SendMessageHandler : ISendMessageHandler
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SendMessageHandler(
        IChatRoomRepository chatRoomRepository,
        IMessageRepository messageRepository,
        IUnitOfWork unitOfWork)
    {
        _chatRoomRepository = chatRoomRepository;
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MessageDto> HandleAsync(
        SendMessageCommand command,
        Guid currentUserId,
        CancellationToken cancellationToken)
    {
        var chatRoom = await _chatRoomRepository.GetByIdAsync(
            command.ChatRoomId,
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

        var message = new Message(
            command.ChatRoomId,
            currentUserId,
            command.Content);

        await _messageRepository.AddAsync(
            message,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return MessageMapper.ToDto(message);
    }
}