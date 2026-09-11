using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Users.Commands.UpdateUser;

public interface IUpdateUserHandler
{
    Task<UserDto> HandleAsync(
        UpdateUserCommand command,
        CancellationToken cancellationToken);
}