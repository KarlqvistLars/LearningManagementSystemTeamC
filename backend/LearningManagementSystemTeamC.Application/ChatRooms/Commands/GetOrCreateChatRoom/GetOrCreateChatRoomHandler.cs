using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.ChatRooms;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.UserInfos;
using LearningManagementSystemTeamC.Domain.Users;
using InvalidOperationException =
    LearningManagementSystemTeamC.Domain.Common.Exceptions.InvalidOperationException;

namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.GetOrCreateChatRoom;

public class GetOrCreateChatRoomHandler : IGetOrCreateChatRoomHandler
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IChatRoomReadRepository _chatRoomReadRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetOrCreateChatRoomHandler(
        IChatRoomRepository chatRoomRepository,
        IChatRoomReadRepository chatRoomReadRepository,
        IUserRepository userRepository,
        IUserInfoRepository userInfoRepository,
        IUnitOfWork unitOfWork)
    {
        _chatRoomRepository = chatRoomRepository;
        _chatRoomReadRepository = chatRoomReadRepository;
        _userRepository = userRepository;
        _userInfoRepository = userInfoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ChatRoomDto> HandleAsync(
        GetOrCreateChatRoomCommand command,
        Guid currentUserId,
        CancellationToken cancellationToken)
    {
        if (command.TargetUserId == currentUserId)
            throw new DomainException(
                ChatRoomRules.CannotChatWithSelfCode,
                ChatRoomRules.CannotChatWithSelfMessage);

        var targetUser = await _userRepository.GetByIdAsync(
            command.TargetUserId,
            cancellationToken) ?? throw new NotFoundException(
                UserRules.UserNotFoundCode,
                UserRules.UserNotFoundMessage);

        var targetUserInfo = await _userInfoRepository.GetByUserIdAsync(
            targetUser.Id,
            cancellationToken) ?? throw new NotFoundException(
                UserInfoRules.UserInfoNotFoundCode,
                UserInfoRules.UserInfoNotFoundMessage);

        var currentUserInfo = await _userInfoRepository.GetByUserIdAsync(
            currentUserId,
            cancellationToken) ?? throw new NotFoundException(
            UserInfoRules.UserInfoNotFoundCode,
            UserInfoRules.UserInfoNotFoundMessage);

        var existingChatRoom =
            await _chatRoomRepository.GetDirectChatRoomAsync(
                currentUserId,
                command.TargetUserId,
                cancellationToken);

        if (existingChatRoom is not null)
        {
            var existingReadModel =
                await _chatRoomReadRepository.GetByIdAsync(
                    existingChatRoom.Id,
                    cancellationToken);

            return existingReadModel is null
                ? throw new InvalidOperationException(
                    ChatRoomRules.CreateReadFailedCode,
                    ChatRoomRules.CreateReadFailedMessage)
                : ChatRoomMapper.ToDto(existingReadModel);
        }

        var names = new[]
        {
            currentUserInfo.FirstName,
            targetUserInfo.FirstName
        }
        .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
        .ToArray();

        var chatRoom = new ChatRoom(
            $"{names[0]} & {names[1]}",
            currentUserId);

        chatRoom.AddMember(currentUserId);
        chatRoom.AddMember(command.TargetUserId);

        await _chatRoomRepository.AddAsync(
            chatRoom,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        var readModel =
            await _chatRoomReadRepository.GetByIdAsync(
                chatRoom.Id,
                cancellationToken);

        return readModel is null
            ? throw new InvalidOperationException(
                ChatRoomRules.CreateReadFailedCode,
                ChatRoomRules.CreateReadFailedMessage)
            : ChatRoomMapper.ToDto(readModel);
    }
}