using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.ChatRooms;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.AddChatRoomMember;

public class AddChatRoomMemberHandler : IAddChatRoomMemberHandler
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddChatRoomMemberHandler(
        IChatRoomRepository chatRoomRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _chatRoomRepository = chatRoomRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(
        AddChatRoomMemberCommand command,
        Guid currentUserId,
        CancellationToken cancellationToken)
    {
        var chatRoom = await _chatRoomRepository.GetByIdAsync(
            command.ChatRoomId,
            cancellationToken) ?? throw new NotFoundException(
                ChatRoomRules.NotFoundCode,
                ChatRoomRules.NotFoundMessage);

        if (chatRoom.CreatedBy != currentUserId)
        {
            throw new UnauthorizedException(
                ChatRoomRules.NotOwnerCode,
                ChatRoomRules.NotOwnerMessage);
        }

        var user = await _userRepository.GetByIdAsync(
            command.UserId,
            cancellationToken) ?? throw new NotFoundException(
                UserRules.UserNotFoundCode,
                UserRules.UserNotFoundMessage);

        chatRoom.AddMember(user.Id);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);
    }
}