using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;
using LearningManagementSystemTeamC.Application.Users.Commands.UpdateUser;
using LearningManagementSystemTeamC.Application.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;


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
        var details = createUserValidator.Validate(command, cancellationToken);

        if (details.Count > 0)
            return BadRequest(ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));

        var userDto = await createUserHandler.Handle(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, ApiResponse<UserDto>.Ok(userDto));
    }

    [Authorize(Policy = PolicyConstants.AuthenticatedUser)]
    [HttpGet("{id:guid}", Name = EndpointNameConstants.GetUserById)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, [FromServices] IGetUserByIdHandler getUserByIdHandler, CancellationToken cancellationToken)
    {
        var userDto = await getUserByIdHandler.Handle(new GetUserByIdQuery(id), cancellationToken);
        return Ok(ApiResponse<UserDto>.Ok(userDto));
    }

    [Authorize(Policy = PolicyConstants.AuthenticatedUser)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        UpdateUserCommand command,
        [FromServices] IUpdateUserHandler updateUserHandler,
        [FromServices] IValidator<UpdateUserCommand> updateUserValidator,
        CancellationToken cancellationToken)
    {
        var commandWithId = command with {
            UserId = id
        };

        var details = updateUserValidator.Validate(commandWithId, cancellationToken);

        if (details.Count > 0)
            return BadRequest(ApiResponse<Dictionary<string, string[]>>.Fail(
                ExceptionConstants.ValidationFailedCode,
                ExceptionConstants.ValidationFailedMessage,
                details));

        var userDto = await updateUserHandler.Handle(
            commandWithId,
            cancellationToken);

        return Ok(ApiResponse<UserDto>.Ok(userDto));
    }
}
