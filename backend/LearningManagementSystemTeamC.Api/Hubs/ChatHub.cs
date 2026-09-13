using LearningManagementSystemTeamC.Api.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace LearningManagementSystemTeamC.Api.Hubs;

[Authorize(Policy = PolicyConstants.AuthenticatedUser)]
public class ChatHub : Hub
{
    public async Task JoinRoom(Guid chatRoomId)
    {
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