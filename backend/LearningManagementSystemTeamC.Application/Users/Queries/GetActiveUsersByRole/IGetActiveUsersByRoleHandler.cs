using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Users.Queries.GetActiveUsersByRole;

public interface IGetActiveUsersByRoleHandler
{
    Task<IReadOnlyList<UserSimplifiedDto>> HandleAsync(GetActiveUsersByRoleQuery query, CancellationToken cancellationToken);
}
