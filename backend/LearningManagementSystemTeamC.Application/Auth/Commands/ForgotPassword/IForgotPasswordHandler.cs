namespace LearningManagementSystemTeamC.Application.Auth.Commands.ForgotPassword;

public interface IForgotPasswordHandler
{
    Task HandleAsync(
        ForgotPasswordCommand command,
        CancellationToken cancellationToken);
}