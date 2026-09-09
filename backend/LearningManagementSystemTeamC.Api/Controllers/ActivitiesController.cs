using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Activities.CreateActivity;
using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModuleId;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
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

    [HttpGet("{activityId}")]
    public async Task<IActionResult> GetByModuleAndActivity(
        Guid moduleId,
        Guid activityId,
        [FromServices] IGetActivitiesByModuleIdHandler getActivitiesByModuleHandler,
        CancellationToken cancellationToken)
    {
        var activities = await getActivitiesByModuleHandler.Handle(
            new GetActivitiesByModuleIdQuery(moduleId),
            cancellationToken);
        var activity = activities.FirstOrDefault(a => a.Id == activityId);
        if (activity == null)
        {
            return NotFound(ApiResponse<ActivityDto>.Fail(
                ExceptionConstants.NotFoundCode,
                ExceptionConstants.NotFoundMessage));
        }
        return Ok(ApiResponse<ActivityDto>.Ok(activity));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateActivityCommand command,
        [FromServices] ICreateActivityHandler createActivityHandler,
        [FromServices] IValidator<CreateActivityCommand> createActivityValidator,
        CancellationToken cancellationToken)
    {
        var validationResult = createActivityValidator.Validate(command, cancellationToken);
        if (validationResult.Count > 0)
        {
            return BadRequest(
                ApiResponse<ActivityDto>.Fail(
                ExceptionConstants.ValidationFailedCode,
                ExceptionConstants.ValidationFailedMessage,
                validationResult));
        }

        var activityDto = await createActivityHandler.Handle(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetByModuleAndActivity),
            new { moduleId = command.ModuleId, activityId = activityDto.Id },
            ApiResponse<ActivityDto>.Ok(activityDto));
    }
}
