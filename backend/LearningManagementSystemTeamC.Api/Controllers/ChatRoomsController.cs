using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Application.ChatRooms.Commands.AddChatRoomMember;
using LearningManagementSystemTeamC.Application.ChatRooms.Commands.CreateChatRoom;
using LearningManagementSystemTeamC.Application.ChatRooms.Commands.DeleteChatRoom;
using LearningManagementSystemTeamC.Application.ChatRooms.Commands.GetOrCreateChatRoom;
using LearningManagementSystemTeamC.Application.ChatRooms.Commands.RemoveChatRoomMember;
using LearningManagementSystemTeamC.Application.ChatRooms.Queries.GetChatRoomById;
using LearningManagementSystemTeamC.Application.ChatRooms.Queries.GetMyChatRooms;
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

    /// <summary>
    /// Creates a new chat room for the authenticated user.
    /// </summary>
    /// <param name="command">The data used to create the chat room.</param>
    /// <returns>The newly created chat room.</returns>
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

        return CreatedAtAction(
            nameof(GetById),
            new { id = chatRoomDto.Id },
            ApiResponse<ChatRoomDto>.Ok(chatRoomDto));
    }

    /// <summary>
    /// Gets a chat room by its ID.
    /// </summary>
    /// <param name="id">The ID of the chat room.</param>
    /// <returns>The requested chat room.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        [FromServices] IGetChatRoomByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var chatRoomDto = await handler.HandleAsync(
            new GetChatRoomByIdQuery(id),
            cancellationToken);

        return Ok(ApiResponse<ChatRoomDto>.Ok(chatRoomDto));
    }

    /// <summary>
    /// Gets all chat rooms belonging to the authenticated user.
    /// </summary>
    /// <returns>A list of the user's chat rooms.</returns>
    [HttpGet]
    public async Task<IActionResult> GetMyChatRooms(
        [FromServices] IGetMyChatRoomsHandler handler,
        CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();

        var chatRoomDtos = await handler.HandleAsync(
            new GetMyChatRoomsQuery(userId),
            cancellationToken);

        return Ok(
            ApiResponse<IReadOnlyList<ChatRoomDto>>.Ok(chatRoomDtos));
    }

    /// <summary>
    /// Deletes a chat room.
    /// </summary>
    /// <param name="chatRoomId">The ID of the chat room to delete.</param>
    /// <returns>A confirmation that the chat room was deleted.</returns>
    [HttpDelete("{chatRoomId:guid}")]
    public async Task<IActionResult> Delete(
        Guid chatRoomId,
        [FromServices] IDeleteChatRoomHandler handler,
        CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();

        var command = new DeleteChatRoomCommand(
            chatRoomId,
            userId);

        await handler.HandleAsync(
            command,
            cancellationToken);

        return Ok(
            ApiResponse<string>.Ok("Deleted"));
    }

    /// <summary>
    /// Adds a member to a chat room.
    /// </summary>
    /// <param name="command">
    /// The chat room and user information used to add the member.
    /// </param>
    /// <returns>A confirmation that the member was added.</returns>
    [HttpPost("members")]
    public async Task<IActionResult> AddMember(
        AddChatRoomMemberCommand command,
        [FromServices] IAddChatRoomMemberHandler handler,
        CancellationToken cancellationToken)
    {
        var currentUserId = User.GetUserId();

        await handler.HandleAsync(
            command,
            currentUserId,
            cancellationToken);

        return Ok(
            ApiResponse<string>.Ok("Member Added"));
    }

    /// <summary>
    /// Removes a member from a chat room.
    /// </summary>
    /// <param name="chatRoomId">The ID of the chat room.</param>
    /// <param name="userId">The ID of the user to remove.</param>
    /// <returns>A confirmation that the member was removed.</returns>
    [HttpDelete("{chatRoomId:guid}/members/{userId:guid}")]
    public async Task<IActionResult> RemoveMember(
        Guid chatRoomId,
        Guid userId,
        [FromServices] IRemoveChatRoomMemberHandler handler,
        CancellationToken cancellationToken)
    {
        var currentUserId = User.GetUserId();

        var command = new RemoveChatRoomMemberCommand(
            chatRoomId,
            userId);

        await handler.HandleAsync(
            command,
            currentUserId,
            cancellationToken);

        return Ok(
            ApiResponse<string>.Ok("Member Removed"));
    }

    /// <summary>
    /// Gets an existing chat room between the authenticated user and another user,
    /// or creates one if it does not already exist.
    /// </summary>
    /// <param name="userId">
    /// The ID of the other user participating in the chat.
    /// </param>
    /// <returns>The existing or newly created chat room.</returns>
    [HttpPost("{userId:guid}")]
    public async Task<IActionResult> GetOrCreate(
        Guid userId,
        [FromServices] IGetOrCreateChatRoomHandler handler,
        CancellationToken cancellationToken)
    {
        var currentUserId = User.GetUserId();

        var command = new GetOrCreateChatRoomCommand(userId);

        var chatRoomDto = await handler.HandleAsync(
            command,
            currentUserId,
            cancellationToken);

        return Ok(
            ApiResponse<ChatRoomDto>.Ok(chatRoomDto));
    }
}