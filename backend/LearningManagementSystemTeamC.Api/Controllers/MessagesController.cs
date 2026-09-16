using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Api.Hubs;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Messages.Commands.SendMessage;
using LearningManagementSystemTeamC.Application.Messages.Queries.GetMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/chatrooms/{chatRoomId:guid}/messages")]
[Authorize]
public class MessagesController : ControllerBase
{
    public MessagesController()
    {
    }

    /// <summary>
    /// Sends a message to a chat room.
    /// </summary>
    /// <param name="chatRoomId">The ID of the chat room.</param>
    /// <param name="request">The message content to send.</param>
    /// <returns>The newly created message.</returns>
    [HttpPost]
    public async Task<IActionResult> SendMessage(
        Guid chatRoomId,
        [FromBody] SendMessageRequest request,
        [FromServices] ISendMessageHandler sendMessageHandler,
        [FromServices] IValidator<SendMessageCommand> sendMessageValidator,
        [FromServices] IHubContext<ChatHub> hubContext,
        CancellationToken cancellationToken)
    {
        var command = new SendMessageCommand(
            chatRoomId,
            request.Content);

        var details = sendMessageValidator.Validate(command);

        if (details.Count > 0)
        {
            return BadRequest(
                ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));
        }

        var userId = User.GetUserId();

        var messageDto = await sendMessageHandler.HandleAsync(
            command,
            userId,
            cancellationToken);

        await hubContext.Clients
            .Group(chatRoomId.ToString())
            .SendAsync(
                "ReceiveMessage",
                messageDto,
                cancellationToken);

        return Ok(
            ApiResponse<MessageDto>.Ok(messageDto));
    }

    /// <summary>
    /// Gets all messages from a chat room.
    /// </summary>
    /// <param name="chatRoomId">The ID of the chat room.</param>
    /// <returns>A list of messages belonging to the chat room.</returns>
    [HttpGet]
    public async Task<IActionResult> GetMessages(
        Guid chatRoomId,
        [FromServices] IGetMessagesHandler getMessagesHandler,
        CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();

        var query = new GetMessagesQuery(chatRoomId);

        var messageDtos = await getMessagesHandler.HandleAsync(
            query,
            userId,
            cancellationToken);

        return Ok(
            ApiResponse<IReadOnlyList<MessageDto>>.Ok(messageDtos));
    }
}