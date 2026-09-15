using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Application.Activities.Command.CreateActivity;
using LearningManagementSystemTeamC.Application.Activities.Command.EditActivity;
using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModuleId;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        var userId = User.GetUserId();
        var role = User.GetRole();

        var activities = await getActivitiesByModuleHandler.Handle(
            new GetActivitiesByModuleIdQuery(moduleId, userId, role),
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

    [HttpPost]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
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
            new { moduleId = command.ModuleId, activityId = activityDto.Id },
            ApiResponse<ActivityDto>.Ok(activityDto));
    }

    [HttpPut("{activityId:guid}")]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    public async Task<IActionResult> Edit(
        Guid moduleId,
        Guid activityId,
        EditActivityCommand command,
        [FromServices] IEditActivityHandler editActivityHandler,
        [FromServices] IValidator<EditActivityCommand> editActivityValidator,
        CancellationToken cancellationToken)
    {
        var editCommand = command with { Id = activityId, ModuleId = moduleId };

        var validationResult = editActivityValidator.Validate(editCommand);
        if (validationResult.Count > 0)
        {
            return BadRequest(
                ApiResponse<ActivityDto>.Fail(
                ExceptionConstants.ValidationFailedCode,
                ExceptionConstants.ValidationFailedMessage,
                validationResult));
        }

        var activityDto = await editActivityHandler.Handle(
            editCommand,
            cancellationToken);

        return Ok(ApiResponse<ActivityDto>.Ok(activityDto));
    }
}
