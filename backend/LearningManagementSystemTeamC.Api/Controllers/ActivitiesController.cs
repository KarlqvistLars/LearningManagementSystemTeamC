using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModule;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/modules/{moduleId}/activities")]
public class ActivitiesController : ControllerBase
{
    public ActivitiesController() { }

    [HttpGet]
    public async Task<IActionResult> GetByModule(
        Guid moduleId,
        [FromServices] IGetActivitiesByModuleHandler getActivitiesByModuleHandler,
        CancellationToken cancellationToken)
    {
        var activities = await getActivitiesByModuleHandler.Handle(
            new GetActivitiesByModuleQuery(moduleId),
            cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ActivityDto>>.Ok(activities));
    }
}
