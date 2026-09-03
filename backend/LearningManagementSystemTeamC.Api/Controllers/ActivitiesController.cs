using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModule;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/modules/{moduleId}/activities")]
public class ActivitiesController : ControllerBase
{
    private readonly GetActivitiesByModuleHandler _getActivitiesByModuleHandler;

    public ActivitiesController(GetActivitiesByModuleHandler getActivitiesByModuleHandler)
    {
        _getActivitiesByModuleHandler = getActivitiesByModuleHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetByModule(
        Guid moduleId,
        CancellationToken cancellationToken)
    {
        var activitiesDto = await _getActivitiesByModuleHandler.Handle(
            new GetActivitiesByModuleQuery(moduleId));

        return Ok(
            ApiResponse<IReadOnlyList<ActivityDto>>.Ok(activitiesDto));
    }
}
