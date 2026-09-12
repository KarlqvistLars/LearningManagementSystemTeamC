using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Messages.Commands.SendMessage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/chatrooms/{chatRoomId:guid}/messages")]
[Authorize]
public class MessagesController : ControllerBase
{

    public MessagesController()
    {
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(
        Guid chatRoomId,
        [FromBody] SendMessageRequest request,
        [FromServices] ISendMessageHandler sendMessageHandler,
        [FromServices] IValidator<SendMessageCommand> sendMessageValidator,
        CancellationToken cancellationToken)
    {
        var command = new SendMessageCommand(
            chatRoomId,
            request.Content);

        var details = sendMessageValidator.Validate(command);

        if (details.Count > 0)
        {
            return BadRequest(ApiResponse<Dictionary<string, string[]>>.Fail(
                ExceptionConstants.ValidationFailedCode,
                ExceptionConstants.ValidationFailedMessage,
                details));
        }

        var userId = User.GetUserId();

        var message = await sendMessageHandler.HandleAsync(
            command,
            userId,
            cancellationToken);

        return Ok(message);
    }
}