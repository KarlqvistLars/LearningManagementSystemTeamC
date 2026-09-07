using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Domain.Roles;

namespace LearningManagementSystemTeamC.Api.Common.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services
            .AddAuthorizationBuilder()
            .AddPolicy(PolicyConstants.AuthenticatedUser, policy => policy.RequireAuthenticatedUser())
            .AddPolicy(
                PolicyConstants.TeacherOnly,
                policy => policy.RequireRole(RoleRules.TeacherRoleCode)
            );

        return services;
    }
}
