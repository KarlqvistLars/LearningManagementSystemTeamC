using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Auth.Commands.Login;

public interface ILoginHandler
{
    Task<LoginResultDto> HandleAsync(LoginCommand command, CancellationToken cancellationToken);
}
