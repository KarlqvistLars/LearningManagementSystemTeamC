using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;


[ApiController]
[Route("api/users")]
public class UserController: ControllerBase
{
    private readonly CreateUserHandler _createUserHandler;
    private readonly CreateUserValidator _createUserValidator;

    public UserController(CreateUserHandler createUserHandler, CreateUserValidator createUserValidator)
    {
        _createUserHandler = createUserHandler;
        _createUserValidator = createUserValidator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var details = _createUserValidator.Validate(command);

        if (details.Count > 0)
        {
            return BadRequest(ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));
        }

        var userDto = await _createUserHandler.Handle(command, cancellationToken);

        return CreatedAtAction(nameof(Create), new { id = userDto.Id }, ApiResponse<UserDto>.Ok(userDto));
    }

}
