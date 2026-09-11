using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Roles.Queries.GetRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[Authorize(Policy = PolicyConstants.AuthenticatedUser)]
[ApiController]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRoles(
        [FromServices] IGetRolesHandler getRolesHandler,
        CancellationToken cancellationToken)
    {
        var roles = await getRolesHandler.HandleAsync(
            new GetRolesQuery(),
            cancellationToken);

        return Ok(
            ApiResponse<IReadOnlyList<RoleDto>>.Ok(roles));
    }
}