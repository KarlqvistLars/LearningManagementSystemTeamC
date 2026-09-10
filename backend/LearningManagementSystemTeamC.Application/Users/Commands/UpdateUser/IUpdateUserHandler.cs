using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Users.Commands.UpdateUser;

public interface IUpdateUserHandler
{
    Task<UserDto> Handle(
        UpdateUserCommand command,
        CancellationToken cancellationToken);
}