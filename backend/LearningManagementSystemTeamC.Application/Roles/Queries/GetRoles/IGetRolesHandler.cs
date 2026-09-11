using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Roles.Queries.GetRoles;

public interface IGetRolesHandler
{
    Task<IReadOnlyList<RoleDto>> HandleAsync(
        GetRolesQuery query,
        CancellationToken cancellationToken);
}