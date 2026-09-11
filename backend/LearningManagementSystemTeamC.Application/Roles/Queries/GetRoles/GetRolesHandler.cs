using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;

namespace LearningManagementSystemTeamC.Application.Roles.Queries.GetRoles;

public class GetRolesHandler : IGetRolesHandler
{
    private readonly IRoleRepository _roleRepository;

    public GetRolesHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IReadOnlyList<RoleDto>> HandleAsync(
        GetRolesQuery query,
        CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAllActiveAsync(
            cancellationToken);

        return roles
            .Select(RoleMapper.ToDto)
            .ToList();
    }
}