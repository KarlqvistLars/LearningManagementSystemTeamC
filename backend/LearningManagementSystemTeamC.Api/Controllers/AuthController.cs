using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    public AuthController()
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command, [FromServices] ICreateUserHandler createUserHandler, [FromServices] IValidator<CreateUserCommand> createUserValidator, CancellationToken cancellationToken)
    {
        var details = createUserValidator.Validate(command);

        if (details.Count > 0)
            return BadRequest(ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));

        var userDto = await createUserHandler.Handle(command, cancellationToken);
        return CreatedAtRoute(EndpointNameConstants.GetUserById, new { id = userDto.Id }, ApiResponse<UserDto>.Ok(userDto));
    }
}
