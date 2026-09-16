using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Auth.Commands.ForgotPassword;
using LearningManagementSystemTeamC.Application.Auth.Commands.Login;
using LearningManagementSystemTeamC.Application.Auth.Commands.RegisterUser;
using LearningManagementSystemTeamC.Application.Auth.Commands.ResetPassword;
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

    /// <summary>
    /// Registers a new user account as student.
    /// </summary>
    /// <param name="command">The user registration data.</param>
    /// <returns>The newly registered user's information.</returns>
    [HttpPost]
    public async Task<IActionResult> Create(
        RegisterUserCommand command,
        [FromServices] IRegisterUserHandler registerUserHandler,
        [FromServices] IValidator<RegisterUserCommand> registerUserValidator,
        CancellationToken cancellationToken)
    {
        var details = registerUserValidator.Validate(command);

        if (details.Count > 0)
            return BadRequest(ApiResponse<Dictionary<string, string[]>>.Fail(
                ExceptionConstants.ValidationFailedCode,
                ExceptionConstants.ValidationFailedMessage,
                details));

        var userDto = await registerUserHandler.HandleAsync(
            command,
            cancellationToken);

        return CreatedAtRoute(
            EndpointNameConstants.GetUserById,
            new { id = userDto.Id },
            ApiResponse<UserDto>.Ok(userDto));
    }

    /// <summary>
    /// Authenticates a user and returns an access token.
    /// </summary>
    /// <param name="command">The user's login credentials.</param>
    /// <returns>The authentication result containing the access token.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginCommand command,
        [FromServices] ILoginHandler loginHandler,
        [FromServices] IValidator<LoginCommand> loginValidator,
        CancellationToken cancellationToken)
    {
        var details = loginValidator.Validate(command);

        if (details.Count > 0)
            return BadRequest(ApiResponse<Dictionary<string, string[]>>.Fail(
                ExceptionConstants.ValidationFailedCode,
                ExceptionConstants.ValidationFailedMessage,
                details));

        var result = await loginHandler.HandleAsync(
            command,
            cancellationToken);

        return Ok(ApiResponse<LoginResultDto>.Ok(result));
    }

    /// <summary>
    /// Sends a password reset email to the specified user.
    /// </summary>
    /// <param name="command">The email address associated with the user account.</param>
    /// <returns>A confirmation that the password reset email was sent.</returns>
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(
        ForgotPasswordCommand command,
        [FromServices] IForgotPasswordHandler forgotPasswordHandler,
        [FromServices] IValidator<ForgotPasswordCommand> forgotPasswordValidator,
        CancellationToken cancellationToken)
    {
        var details = forgotPasswordValidator.Validate(command);

        if (details.Count > 0)
        {
            return BadRequest(
                ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));
        }

        await forgotPasswordHandler.HandleAsync(
            command,
            cancellationToken);

        return Ok(
            ApiResponse<string>.Ok(
                ForgotPasswordRules.ResetEmailSentMessage));
    }

    /// <summary>
    /// Resets a user's password using a valid reset token.
    /// </summary>
    /// <param name="command">
    /// The password reset data containing the reset token and new password.
    /// </param>
    /// <returns>A confirmation that the password was successfully reset.</returns>
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(
        ResetPasswordCommand command,
        [FromServices] IResetPasswordHandler resetPasswordHandler,
        [FromServices] IValidator<ResetPasswordCommand> resetPasswordValidator,
        CancellationToken cancellationToken)
    {
        var details = resetPasswordValidator.Validate(command);

        if (details.Count > 0)
        {
            return BadRequest(
                ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));
        }

        await resetPasswordHandler.HandleAsync(
            command,
            cancellationToken);

        return Ok(
            ApiResponse<string>.Ok(
                ResetPasswordRules.ResetPasswordSuccessMessage));
    }
}