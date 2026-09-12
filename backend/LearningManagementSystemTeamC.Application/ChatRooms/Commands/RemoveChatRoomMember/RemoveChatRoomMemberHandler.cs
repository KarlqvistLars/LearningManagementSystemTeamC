using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.ChatRooms;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.RemoveChatRoomMember;

public class RemoveChatRoomMemberHandler
    : IRemoveChatRoomMemberHandler
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveChatRoomMemberHandler(
        IChatRoomRepository chatRoomRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _chatRoomRepository = chatRoomRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(
        RemoveChatRoomMemberCommand command,
        Guid currentUserId,
        CancellationToken cancellationToken)
    {
        var chatRoom = await _chatRoomRepository.GetByIdAsync(
            command.ChatRoomId,
            cancellationToken) ?? throw new NotFoundException(
                ChatRoomRules.NotFoundCode,
                ChatRoomRules.NotFoundMessage);

        var user = await _userRepository.GetByIdAsync(
            command.TargetUserId,
            cancellationToken) ?? throw new NotFoundException(
                UserRules.UserNotFoundCode,
                UserRules.UserNotFoundMessage);

        var isRemovingSelf =
            command.TargetUserId == currentUserId;

        var isOwner =
            chatRoom.CreatedBy == currentUserId;

        if (!isRemovingSelf && !isOwner)
            throw new UnauthorizedException(
                ChatRoomRules.NotOwnerCode,
                ChatRoomRules.NotOwnerMessage);

        chatRoom.RemoveMember(
            command.TargetUserId);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);
    }
}