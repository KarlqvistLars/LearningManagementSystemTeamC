using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Application.ChatRooms.Commands.CreateChatRoom;
using LearningManagementSystemTeamC.Application.ChatRooms.Queries.GetChatRoomById;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/chatrooms")]
[Authorize(Policy = PolicyConstants.AuthenticatedUser)]
public class ChatRoomsController : ControllerBase
{
    public ChatRoomsController()
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateChatRoomCommand command,
        [FromServices] ICreateChatRoomHandler createChatRoomHandler,
        [FromServices] IValidator<CreateChatRoomCommand> createChatRoomValidator,
        CancellationToken cancellationToken)
    {
        var details = createChatRoomValidator.Validate(command);
        if (details.Count > 0)
        {
            return BadRequest(
                ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));
        }

        var creatorId = User.GetUserId();

        var chatRoomDto = await createChatRoomHandler.HandleAsync(
            command,
            creatorId,
            cancellationToken);
        // TODO: to createdataction
        return CreatedAtAction(nameof(GetById), new { id = chatRoomDto.Id }, ApiResponse<ChatRoomDto>.Ok(chatRoomDto));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
    Guid id,
    [FromServices] IGetChatRoomByIdHandler handler,
    CancellationToken cancellationToken)
    {
        var chatRoomDto = await handler.HandleAsync(
            id,
            cancellationToken);

        return Ok(ApiResponse<ChatRoomDto>.Ok(chatRoomDto));
    }
}