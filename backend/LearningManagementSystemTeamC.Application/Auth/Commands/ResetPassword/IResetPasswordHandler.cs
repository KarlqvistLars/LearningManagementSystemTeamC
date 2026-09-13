namespace LearningManagementSystemTeamC.Application.Auth.Commands.ResetPassword;

public interface IResetPasswordHandler
{
    Task HandleAsync(
        ResetPasswordCommand command,
        CancellationToken cancellationToken);
}