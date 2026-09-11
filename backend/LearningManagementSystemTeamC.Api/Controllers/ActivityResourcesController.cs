using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.ActivityResources.Command.CreateActivityResource;
using LearningManagementSystemTeamC.Application.ActivityResources.Queries.GetResourcesByActivityId;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
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

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateActivityResourceCommand command,
        [FromServices] ICreateActivityResourceHandler createActivityResourceHandler,
        [FromServices] IValidator<CreateActivityResourceCommand> createActivityValidator,
        CancellationToken cancellationToken)
    {
        var validationResult = createActivityValidator.Validate(command, cancellationToken);
        if (validationResult.Count > 0)
        {
            return BadRequest(
                ApiResponse<ActivityResourceDto>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    validationResult)
                );
        }

        var activityResourceDto = await createActivityResourceHandler.Handle(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetByActivityResources),
            new { activityId = command.ActivityId, resourceId = activityResourceDto.Id },
            ApiResponse<ActivityResourceDto>.Ok(activityResourceDto));
    }
}
