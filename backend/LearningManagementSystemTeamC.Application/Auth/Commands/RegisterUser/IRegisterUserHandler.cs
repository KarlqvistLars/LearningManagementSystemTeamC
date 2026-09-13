using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Auth.Commands.RegisterUser;

public interface IRegisterUserHandler
{
    Task<UserDto> HandleAsync(RegisterUserCommand command, CancellationToken cancellationToken);
}
