using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Auth.Commands.RegisterUser;

public interface IRegisterUserHandler
{
    Task<UserDto> Handle(RegisterUserCommand command, CancellationToken cancellationToken);
}
