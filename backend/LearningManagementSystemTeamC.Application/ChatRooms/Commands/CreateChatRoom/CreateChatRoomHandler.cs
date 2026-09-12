using LearningManagementSystemTeamC.Application.ChatRooms;
using LearningManagementSystemTeamC.Application.ChatRooms.Commands.CreateChatRoom;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.ChatRooms;

public class CreateChatRoomHandler : ICreateChatRoomHandler
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateChatRoomHandler(
        IChatRoomRepository chatRoomRepository,
        IUnitOfWork unitOfWork)
    {
        _chatRoomRepository = chatRoomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ChatRoomDto> HandleAsync(
        CreateChatRoomCommand command,
        Guid creatorId,
        CancellationToken cancellationToken)
    {
        var chatRoom = new ChatRoom(command.Name);

        chatRoom.AddMember(creatorId);

        foreach (var memberId in command.MemberIds.Distinct())
        {
            if (memberId != creatorId)
            {
                chatRoom.AddMember(memberId);
            }
        }

        await _chatRoomRepository.AddAsync(
            chatRoom,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return chatRoom.Id;
    }
}