using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;

public interface ICreateUserHandler
{
    Task<UserDto> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken);
}
