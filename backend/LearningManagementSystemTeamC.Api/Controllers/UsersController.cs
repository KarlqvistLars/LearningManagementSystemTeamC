using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;
using LearningManagementSystemTeamC.Application.Users.Commands.DeleteUser;
using LearningManagementSystemTeamC.Application.Users.Commands.UpdateUser;
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

    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command, [FromServices] ICreateUserHandler createUserHandler, [FromServices] IValidator<CreateUserCommand> createUserValidator, CancellationToken cancellationToken)
    {
        var details = createUserValidator.Validate(command);

        if (details.Count > 0)
            return BadRequest(ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));

        var userDto = await createUserHandler.HandleAsync(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, ApiResponse<UserDto>.Ok(userDto));
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromServices] IGetUsersHandler getUsersHandler, CancellationToken cancellationToken)
    {
        var userDtos = await getUsersHandler.HandleAsync(new GetUsersQuery(), cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<UserDto>>.Ok(userDtos));
    }

    [HttpGet("{id:guid}", Name = EndpointNameConstants.GetUserById)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, [FromServices] IGetUserByIdHandler getUserByIdHandler, CancellationToken cancellationToken)
    {
        var userDto = await getUserByIdHandler.HandleAsync(new GetUserByIdQuery(id), cancellationToken);
        return Ok(ApiResponse<UserDto>.Ok(userDto));
    }

    // TODO: this is currently an security debt, waitting for extension method on other's branch, get Id and Role from jwt then compare
    // Student can only update own user, teacher can update all
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

        var details = updateUserValidator.Validate(commandWithId);

        if (details.Count > 0)
            return BadRequest(ApiResponse<Dictionary<string, string[]>>.Fail(
                ExceptionConstants.ValidationFailedCode,
                ExceptionConstants.ValidationFailedMessage,
                details));

        var userDto = await updateUserHandler.HandleAsync(
            commandWithId,
            cancellationToken);

        return Ok(ApiResponse<UserDto>.Ok(userDto));
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> Delete(
    [FromRoute] Guid userId,
    [FromServices] IDeleteUserHandler deleteUserHandler,
    CancellationToken cancellationToken)
    {
        await deleteUserHandler.HandleAsync(new DeleteUserCommand(userId), cancellationToken);

        return Ok(ApiResponse<string>.Ok("User deleted"));
    }
}
