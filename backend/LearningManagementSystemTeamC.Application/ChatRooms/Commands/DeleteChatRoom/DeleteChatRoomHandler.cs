using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.ChatRooms;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.DeleteChatRoom;

public class DeleteChatRoomHandler : IDeleteChatRoomHandler
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteChatRoomHandler(
        IChatRoomRepository chatRoomRepository,
        IUnitOfWork unitOfWork)
    {
        _chatRoomRepository = chatRoomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(
        DeleteChatRoomCommand command,
        CancellationToken cancellationToken)
    {
        var chatRoom = await _chatRoomRepository.GetByIdAsync(
            command.ChatRoomId,
            cancellationToken) ?? throw new NotFoundException(
                ChatRoomRules.NotFoundCode,
                ChatRoomRules.NotFoundMessage);

        if (chatRoom.CreatedBy != command.UserId)
            throw new UnauthorizedException(
                ChatRoomRules.NotOwnerCode,
                ChatRoomRules.NotOwnerMessage);

        _chatRoomRepository.Remove(chatRoom);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);
    }
}