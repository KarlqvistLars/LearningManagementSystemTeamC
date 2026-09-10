using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModuleId;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Authorize(Policy = PolicyConstants.AuthenticatedUser)]
[Route("api/modules/{moduleId}/activities")]
public class ActivitiesController : ControllerBase
{
    public ActivitiesController() { }

    [HttpGet]
    public async Task<IActionResult> GetByModule(
        Guid moduleId,
        [FromServices] IGetActivitiesByModuleIdHandler getActivitiesByModuleHandler,
        CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;

        var activities = await getActivitiesByModuleHandler.Handle(
            new GetActivitiesByModuleIdQuery(moduleId, userId, role),
            cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ActivityDto>>.Ok(activities));
    }
}
