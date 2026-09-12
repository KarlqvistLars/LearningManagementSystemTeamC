using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Application.ChatRooms.Commands.CreateChatRoom;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/chatrooms")]
[Authorize(Policy = "AuthenticatedUser")]
public class ChatRoomsController : ControllerBase
{

    public ChatRoomsController()
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateChatRoomCommand command,
        [FromServices] ICreateChatRoomHandler createChatRoomHandler,
        CancellationToken cancellationToken)
    {
        var creatorId = User.GetUserId();

        var chatRoomDto = await createChatRoomHandler.HandleAsync(
            command,
            creatorId,
            cancellationToken);
        // TODO: to createdataction
        return Ok(ApiResponse<ChatRoomDto>.Ok(chatRoomDto));
    }
}