using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;

public interface ICreateUserHandler
{
    Task<UserDto> Handle(CreateUserCommand command, CancellationToken cancellationToken);
}
