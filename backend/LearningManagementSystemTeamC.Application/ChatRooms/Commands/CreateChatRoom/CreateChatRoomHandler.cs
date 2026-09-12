using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Domain.ChatRooms;
using InvalidOperationException = LearningManagementSystemTeamC.Domain.Common.Exceptions.InvalidOperationException;

namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.CreateChatRoom;

public class CreateChatRoomHandler : ICreateChatRoomHandler
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IChatRoomReadRepository _chatRoomReadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateChatRoomHandler(
        IChatRoomRepository chatRoomRepository,
        IChatRoomReadRepository chatRoomReadRepository,
        IUnitOfWork unitOfWork)
    {
        _chatRoomRepository = chatRoomRepository;
        _chatRoomReadRepository = chatRoomReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ChatRoomDto> HandleAsync(
        CreateChatRoomCommand command,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var chatRoom = new ChatRoom(command.Name, userId);

        chatRoom.AddMember(userId);

        foreach (var memberId in command.MemberIds.Distinct())
        {
            if (memberId != userId)
            {
                chatRoom.AddMember(memberId);
            }
        }

        await _chatRoomRepository.AddAsync(
            chatRoom,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        var chatRoomReadModel =
            await _chatRoomReadRepository.GetByIdAsync(
                chatRoom.Id,
                cancellationToken);

        return chatRoomReadModel is null
            ? throw new InvalidOperationException(ChatRoomRules.CreateReadFailedCode, ChatRoomRules.CreateReadFailedMessage)
            : ChatRoomMapper.ToDto(chatRoomReadModel);
    }
}