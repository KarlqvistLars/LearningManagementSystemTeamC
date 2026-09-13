namespace LearningManagementSystemTeamC.Application.Users.Commands.DeleteUser;

public interface IDeleteUserHandler
{
    Task HandleAsync(
        DeleteUserCommand command,
        CancellationToken cancellationToken);
}
