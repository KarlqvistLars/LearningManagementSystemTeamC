using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModuleId;
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
        [FromServices] IGetActivitiesByModuleIdHandler getActivitiesByModuleHandler,
        CancellationToken cancellationToken)
    {
        var activities = await getActivitiesByModuleHandler.Handle(
            new GetActivitiesByModuleIdQuery(moduleId),
            cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ActivityDto>>.Ok(activities));
    }
}
