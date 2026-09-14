using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.ActivityResources.Command.CreateActivityResource;
using LearningManagementSystemTeamC.Application.ActivityResources.Queries.GetResourcesByActivityId;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Resources.Command.CreateResource;
using LearningManagementSystemTeamC.Application.Resources.Command.UpdateResource;
using LearningManagementSystemTeamC.Application.Resources.Queries.GetAllResources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Authorize(Policy = PolicyConstants.AuthenticatedUser)]
[Route("api")]
public class ResourcesController : ControllerBase
{
    public ResourcesController() { }

    // api/resources
    [HttpGet("resources")]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    public async Task<IActionResult> GetAllResources(
        [FromServices] IGetAllResourcesHandler getAllResourcesHandler,
        CancellationToken cancellationToken)
    {
        var resources = await getAllResourcesHandler.Handle(
            new GetAllResourcesQuery(Guid.Empty), // Assuming you want to get all resources without filtering by ResourceId
            cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ResourceDto>>.Ok(resources));
    }

    // "api/activities/{activityId}/resources"
    [HttpGet("activities/{activityId}/resources")]
    public async Task<IActionResult> GetResourceByActivityId(
        Guid activityId,
        [FromServices] IGetResourcesByActivityIdHandler getResourcesByActivityIdHandler,
        CancellationToken cancellationToken)
    {

        var resources = await getResourcesByActivityIdHandler.Handle(
            new GetResourcesByActivityIdQuery(activityId),
            cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ResourceDto>>.Ok(resources));
    }

    // "api/resources"
    [HttpPost("resources")]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    public async Task<IActionResult> Create(
        [FromBody] CreateResourceCommand command,
        [FromServices] ICreateResourceHandler createResourceHandler,
        [FromServices] IValidator<CreateResourceCommand> createResourceValidator,
        CancellationToken cancellationToken)
    {
        var validationResult =
            createResourceValidator.Validate(command, cancellationToken);
        if (validationResult.Count > 0)
        {
            return BadRequest(
                ApiResponse<ResourceDto>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    validationResult)
                );
        }

        var resourceDto = await createResourceHandler.Handle(
            command,
            cancellationToken);

        return Ok(ApiResponse<ResourceDto>.Ok(resourceDto));
    }

    // "api/resources/{resourceId}"
    [HttpPut("resources/{resourceId:guid}")]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    public async Task<IActionResult> Update(
        Guid resourceId,
        [FromBody] UpdateResourceCommand command,
        [FromServices] IUpdateResourceHandler updateResourceHandler,
        [FromServices] IValidator<UpdateResourceCommand> updateResourceValidator,
        CancellationToken cancellationToken)
    {
        var commandWithId = command with { ResourceId = resourceId };

        var validationResult =
            updateResourceValidator.Validate(commandWithId, cancellationToken);
        if (validationResult.Count > 0)
        {
            return BadRequest(
                ApiResponse<ResourceDto>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    validationResult)
                );
        }
        var updatedResourceDto = await updateResourceHandler.Handle(
            commandWithId,
            cancellationToken);

        if (updatedResourceDto is null)
        {
            return NotFound();
        }
        return Ok(ApiResponse<ResourceDto>.Ok(updatedResourceDto));
    }
}
