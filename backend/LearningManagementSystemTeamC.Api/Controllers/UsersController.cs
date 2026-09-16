using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;
using LearningManagementSystemTeamC.Application.Users.Commands.DeleteUser;
using LearningManagementSystemTeamC.Application.Users.Commands.ToggleUserStatus;
using LearningManagementSystemTeamC.Application.Users.Commands.UpdateUser;
using LearningManagementSystemTeamC.Application.Users.Queries.GetActiveUsersByRole;
using LearningManagementSystemTeamC.Application.Users.Queries.GetUserById;
using LearningManagementSystemTeamC.Application.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[Authorize(Policy = PolicyConstants.AuthenticatedUser)]
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    public UsersController()
    {
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="command">The user creation data.</param>
    /// <returns>The newly created user.</returns>
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateUserCommand command,
        [FromServices] ICreateUserHandler createUserHandler,
        [FromServices] IValidator<CreateUserCommand> createUserValidator,
        CancellationToken cancellationToken)
    {
        var details = createUserValidator.Validate(command);

        if (details.Count > 0)
        {
            return BadRequest(
                ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));
        }

        var userDto = await createUserHandler.HandleAsync(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = userDto.Id },
            ApiResponse<UserDto>.Ok(userDto));
    }

    /// <summary>
    /// Gets users available to the current user.
    /// </summary>
    /// <returns>A list of users.</returns>
    [HttpGet]
    public async Task<IActionResult> GetUsers(
        [FromServices] IGetUsersHandler getUsersHandler,
        CancellationToken cancellationToken)
    {
        var userRole = User.GetRole();

        var userDtos = await getUsersHandler.HandleAsync(
            new GetUsersQuery(),
            userRole,
            cancellationToken);

        return Ok(
            ApiResponse<IReadOnlyList<UserDto>>.Ok(userDtos));
    }

    /// <summary>
    /// Gets a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The requested user.</returns>
    [HttpGet("{id:guid}", Name = EndpointNameConstants.GetUserById)]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        [FromServices] IGetUserByIdHandler getUserByIdHandler,
        CancellationToken cancellationToken)
    {
        var userDto = await getUserByIdHandler.HandleAsync(
            new GetUserByIdQuery(id),
            cancellationToken);

        return Ok(
            ApiResponse<UserDto>.Ok(userDto));
    }

    /// <summary>
    /// Updates an existing user's information.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="command">The updated user information.</param>
    /// <returns>The updated user.</returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        UpdateUserCommand command,
        [FromServices] IUpdateUserHandler updateUserHandler,
        [FromServices] IValidator<UpdateUserCommand> updateUserValidator,
        CancellationToken cancellationToken)
    {
        var commandWithId = command with
        {
            UserId = id
        };

        var userId = User.GetUserId();
        var roleCode = User.GetRole();

        var details = updateUserValidator.Validate(commandWithId);

        if (details.Count > 0)
        {
            return BadRequest(
                ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));
        }

        var userDto = await updateUserHandler.HandleAsync(
            commandWithId,
            userId,
            roleCode,
            cancellationToken);

        return Ok(
            ApiResponse<UserDto>.Ok(userDto));
    }

    /// <summary>
    /// Deletes a user.
    /// </summary>
    /// <param name="userId">The ID of the user to delete.</param>
    /// <returns>A confirmation message indicating that the user was deleted.</returns>
    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid userId,
        [FromServices] IDeleteUserHandler deleteUserHandler,
        CancellationToken cancellationToken)
    {
        await deleteUserHandler.HandleAsync(
            new DeleteUserCommand(userId),
            cancellationToken);

        return Ok(
            ApiResponse<string>.Ok("User deleted"));
    }

    /// <summary>
    /// Toggles a user's active status.
    /// </summary>
    /// <param name="userId">The ID of the user whose status should be changed.</param>
    /// <returns>A confirmation message indicating that the user's status was updated.</returns>
    [HttpPatch("{userId:guid}/status")]
    public async Task<IActionResult> ToggleStatus(
        [FromRoute] Guid userId,
        [FromServices] IToggleUserStatusHandler handler,
        CancellationToken cancellationToken)
    {
        await handler.HandleAsync(
            new ToggleUserStatusCommand(userId),
            cancellationToken);

        return Ok(
            ApiResponse<string>.Ok("User status updated"));
    }

    /// <summary>
    /// Gets all active users with a specific role.
    /// </summary>
    /// <param name="role">The role of the users to retrieve.</param>
    /// <returns>A list of active users with the specified role.</returns>
    [HttpGet("{role}")]
    public async Task<IActionResult> GetActiveUsersByRole(
        [FromRoute] string role,
        [FromServices] IGetActiveUsersByRoleHandler getUsersHandler,
        CancellationToken cancellationToken)
    {
        var userDtos = await getUsersHandler.HandleAsync(
            new GetActiveUsersByRoleQuery(role),
            cancellationToken);

        return Ok(
            ApiResponse<IReadOnlyList<UserSimplifiedDto>>.Ok(userDtos));
    }
}