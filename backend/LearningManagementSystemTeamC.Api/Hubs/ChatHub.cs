using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Application.ChatRooms;
using LearningManagementSystemTeamC.Domain.ChatRooms;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace LearningManagementSystemTeamC.Api.Hubs;

[Authorize(Policy = PolicyConstants.AuthenticatedUser)]
public class ChatHub : Hub
{
    private readonly IChatRoomRepository _chatRoomRepository;

    public ChatHub(IChatRoomRepository chatRoomRepository)
    {
        _chatRoomRepository = chatRoomRepository;
    }

    public async Task JoinRoom(Guid chatRoomId)
    {
        var user = Context.User
            ?? throw new UnauthorizedException(
            ExceptionConstants.UnauthorizedCode,
            ExceptionConstants.UnauthorizedMessage);

        var userId = user.GetUserId();

        var chatRoom = await _chatRoomRepository.GetByIdAsync(
            chatRoomId,
            Context.ConnectionAborted) ?? throw new NotFoundException(
                ChatRoomRules.NotFoundCode,
                ChatRoomRules.NotFoundMessage);

        var isMember = chatRoom.Members
            .Any(member => member.UserId == userId);

        if (!isMember)
            throw new UnauthorizedException(
                ChatRoomRules.NotMemberCode,
                ChatRoomRules.NotMemberMessage);

        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            chatRoomId.ToString());
    }

    public async Task LeaveRoom(Guid chatRoomId)
    {
        await Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            chatRoomId.ToString());
    }
}