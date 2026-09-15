using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Application.Activities.Command.CreateActivity;
using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivities;
using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModuleId;
using LearningManagementSystemTeamC.Application.Activities.Queries.GetAssignments;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Authorize(Policy = PolicyConstants.AuthenticatedUser)]
[Route("api")]
public class ActivitiesController : ControllerBase
{
    public ActivitiesController() { }

    /// <summary>
    /// Gets all activities belonging to a specific module.
    /// </summary>
    /// <param name="moduleId">The ID of the module.</param>
    /// <returns>A list of activities belonging to the specified module.</returns>
    [HttpGet("modules/{moduleId}/activities")]
    public async Task<IActionResult> GetByModule(
        Guid moduleId,
        [FromServices] IGetActivitiesByModuleIdHandler getActivitiesByModuleHandler,
        CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var role = User.GetRole();

        var activities = await getActivitiesByModuleHandler.Handle(
            new GetActivitiesByModuleIdQuery(moduleId, userId, role),
            cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ActivityDto>>.Ok(activities));
    }

    /// <summary>
    /// Gets a specific activity belonging to a module.
    /// </summary>
    /// <param name="moduleId">The ID of the module.</param>
    /// <param name="activityId">The ID of the activity.</param>
    /// <returns>The requested activity if it belongs to the specified module.</returns>
    [HttpGet("modules/{moduleId}/activities/{activityId}")]
    public async Task<IActionResult> GetByModuleAndActivity(
        Guid moduleId,
        Guid activityId,
        [FromServices] IGetActivitiesByModuleIdHandler getActivitiesByModuleHandler,
        CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var role = User.GetRole();

        var activities = await getActivitiesByModuleHandler.Handle(
            new GetActivitiesByModuleIdQuery(moduleId, userId, role),
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

    /// <summary>
    /// Creates a new activity.
    /// </summary>
    /// <param name="command">The activity data used to create the activity.</param>
    /// <returns>The newly created activity.</returns>
    [HttpPost("activities")]
    public async Task<IActionResult> Create(
        CreateActivityCommand command,
        [FromServices] ICreateActivityHandler createActivityHandler,
        [FromServices] IValidator<CreateActivityCommand> createActivityValidator,
        CancellationToken cancellationToken)
    {
        var validationResult = createActivityValidator.Validate(command);

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
            new
            {
                moduleId = command.ModuleId,
                activityId = activityDto.Id
            },
            ApiResponse<ActivityDto>.Ok(activityDto));
    }

    /// <summary>
    /// Gets all assignments available to the current user.
    /// Teachers receive all assignments.
    /// Students receive assignments from courses they are enrolled in.
    /// </summary>
    /// <returns>A list of assignment details.</returns>
    [HttpGet("activities/assignments")]
    public async Task<IActionResult> GetAssignments(
        [FromServices] IGetAssignmentsHandler getAssignmentsHandler,
        CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var role = User.GetRole();

        var assignments = await getAssignmentsHandler.HandleAsync(
            new GetAssignmentsQuery(),
            userId,
            role,
            cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ActivityDetailsDto>>.Ok(assignments));
    }
}