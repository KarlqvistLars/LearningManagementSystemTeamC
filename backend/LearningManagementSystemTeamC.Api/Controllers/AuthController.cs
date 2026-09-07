using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Auth.Commands.Login;
using LearningManagementSystemTeamC.Application.Auth.Commands.RegisterUser;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
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
    public async Task<IActionResult> Create(RegisterUserCommand command, [FromServices] IRegisterUserHandler registerUserHandler, [FromServices] IValidator<RegisterUserCommand> registerUserValidator, CancellationToken cancellationToken)
    {
        var details = registerUserValidator.Validate(command);

        if (details.Count > 0)
            return BadRequest(ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));

        var userDto = await registerUserHandler.Handle(command, cancellationToken);
        return CreatedAtRoute(EndpointNameConstants.GetUserById, new { id = userDto.Id }, ApiResponse<UserDto>.Ok(userDto));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginCommand command,
        [FromServices] ILoginHandler loginHandler,
        [FromServices] IValidator<LoginCommand> loginValidator,
        CancellationToken cancellationToken
    )
    {
        var details = loginValidator.Validate(command);

        if (details.Count > 0)
            return BadRequest(ApiResponse<Dictionary<string, string[]>>.Fail(
                ExceptionConstants.ValidationFailedCode,
                ExceptionConstants.ValidationFailedMessage,
                details));

        var result = await loginHandler.HandleAsync(command, cancellationToken);

        return Ok(ApiResponse<LoginResultDto>.Ok(result));
    }
}
