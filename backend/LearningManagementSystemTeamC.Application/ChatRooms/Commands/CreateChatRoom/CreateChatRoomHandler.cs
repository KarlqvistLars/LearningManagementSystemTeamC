using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.ChatRooms;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Users;
using InvalidOperationException = LearningManagementSystemTeamC.Domain.Common.Exceptions.InvalidOperationException;

namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.CreateChatRoom;

public class CreateChatRoomHandler : ICreateChatRoomHandler
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IChatRoomReadRepository _chatRoomReadRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateChatRoomHandler(
        IChatRoomRepository chatRoomRepository,
        IChatRoomReadRepository chatRoomReadRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _chatRoomRepository = chatRoomRepository;
        _chatRoomReadRepository = chatRoomReadRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ChatRoomDto> HandleAsync(
        CreateChatRoomCommand command,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var memberIds = command.MemberIds
            .Append(userId)
            .Distinct()
            .ToList();

        var missingUserIds =
            await _userRepository.GetMissingIdsAsync(
                memberIds,
                cancellationToken);

        if (missingUserIds.Count > 0)
            throw new NotFoundException(
                UserRules.UserNotFoundCode,
                UserRules.UserNotFoundMessage);

        var chatRoomExists =
            await _chatRoomRepository.ExistsWithMembersAsync(
                memberIds,
                cancellationToken);

        if (chatRoomExists)
            throw new ConflictException(
                ChatRoomRules.RoomExistsCode,
                ChatRoomRules.RoomExistsMessage);

        var chatRoom = new ChatRoom(
            command.Name,
            userId);

        foreach (var memberId in memberIds)
        {
            chatRoom.AddMember(memberId);
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
            ? throw new InvalidOperationException(
                ChatRoomRules.CreateReadFailedCode,
                ChatRoomRules.CreateReadFailedMessage)
            : ChatRoomMapper.ToDto(chatRoomReadModel);
    }
}