using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.ActivityResources.Queries.GetResourcesByActivityId;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/activities/{activityId}/resources")]
public class ActivityResourcesController : ControllerBase
{
    public ActivityResourcesController() { }

    [HttpGet]
    public async Task<IActionResult> GetByActivityResources(
        Guid activityId,
        [FromServices] IGetResourcesByActivityIdHandler getResourcesByActivityIdHandler,
        CancellationToken cancellationToken)
    {
        var resources = await getResourcesByActivityIdHandler.Handle(
            new GetResourcesByActivityIdQuery(activityId),
            cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ActivityResourceDto>>.Ok(resources));
    }
}
