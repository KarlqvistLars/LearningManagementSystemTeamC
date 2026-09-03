using LearningManagementSystemTeamC.Domain.Roles;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Auth;

public interface IJwtTokenService
{
    string CreateToken(User user, Role role);
}
