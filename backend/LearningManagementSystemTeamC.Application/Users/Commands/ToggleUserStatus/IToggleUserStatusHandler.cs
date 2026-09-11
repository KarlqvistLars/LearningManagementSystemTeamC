namespace LearningManagementSystemTeamC.Application.Users.Commands.ToggleUserStatus;

public interface IToggleUserStatusHandler
{
    Task HandleAsync(
        ToggleUserStatusCommand command,
        CancellationToken cancellationToken);
}