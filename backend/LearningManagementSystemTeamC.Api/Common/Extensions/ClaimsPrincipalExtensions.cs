using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using System.Security.Claims;

namespace LearningManagementSystemTeamC.Api.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(value, out var userId))
        {
            throw new UnauthorizedException(
                ExceptionConstants.UnauthorizedCode,
                ExceptionConstants.UnauthorizedMessage);
        }

        return userId;
    }

    public static string GetRole(this ClaimsPrincipal user)
    {
        var role = user.FindFirstValue(ClaimTypes.Role);

        if (string.IsNullOrWhiteSpace(role))
        {
            throw new UnauthorizedException(
                ExceptionConstants.UnauthorizedCode,
                ExceptionConstants.UnauthorizedMessage);
        }

        return role;
    }
}