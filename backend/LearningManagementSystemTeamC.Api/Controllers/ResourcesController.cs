using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Application.ActivityResources.Queries.GetResourcesByActivityId;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Resources.Command.CreateResource;
using LearningManagementSystemTeamC.Application.Resources.Command.UpdateResource;
using LearningManagementSystemTeamC.Application.Resources.Queries.GetAllResources;
using LearningManagementSystemTeamC.Application.Resources.Queries.GetMySubmission;
using LearningManagementSystemTeamC.Application.Resources.Queries.GetResourceById;
using LearningManagementSystemTeamC.Domain.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Authorize(Policy = PolicyConstants.AuthenticatedUser)]
[Route("api")]
public class ResourcesController : ControllerBase
{
    public ResourcesController() { }

    /// <summary>
    /// Gets all resources.
    /// </summary>
    /// <returns>A list of all resources including their creators.</returns>
    [HttpGet("resources")]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    public async Task<IActionResult> GetAllResources(
        [FromServices] IGetAllResourcesHandler getAllResourcesHandler,
        CancellationToken cancellationToken)
    {
        var resources = await getAllResourcesHandler.HandleAsync(
            new GetAllResourcesQuery(),
            cancellationToken);

        return Ok(
            ApiResponse<IReadOnlyList<ResourceWithCreatorDto>>.Ok(resources));
    }
    /// <summary>
    /// Gets resourse by id number.
    /// </summary>
    /// <param name="resourceId">Id number</param>
    /// <param name="getResourceByIdHandler">EnpointName GetResourceById</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("resources/{resourceId:guid}")]
    public async Task<IActionResult> GetResourceById(
        Guid resourceId,
        [FromServices] IGetResourceByIdHandler getResourceByIdHandler,
        CancellationToken cancellationToken)
    {
        var resource = await getResourceByIdHandler.HandleAsync(
            new GetResourceByIdQuery(resourceId),
            cancellationToken);

        return Ok(ApiResponse<ResourceWithCreatorDto>.Ok(resource));
    }

    /// <summary>
    /// Gets all resources belonging to an activity.
    /// </summary>
    /// <param name="activityId">The ID of the activity.</param>
    /// <returns>A list of resources associated with the specified activity.</returns>
    [HttpGet("activities/{activityId}/resources")]
    public async Task<IActionResult> GetResourceByActivityId(
        Guid activityId,
        [FromServices] IGetResourcesByActivityIdHandler getResourcesByActivityIdHandler,
        CancellationToken cancellationToken)
    {
        var resources = await getResourcesByActivityIdHandler.HandleAsync(
            new GetResourcesByActivityIdQuery(activityId),
            cancellationToken);

        return Ok(
            ApiResponse<IReadOnlyList<ResourceWithCreatorDto>>.Ok(resources));
    }

    /// <summary>
    /// Creates a new resource.
    /// </summary>
    /// <param name="command">The resource creation data.</param>
    /// <returns>The newly created resource.</returns>
    [HttpPost("resources")]
    public async Task<IActionResult> Create(
        [FromBody] CreateResourceCommand command,
        [FromServices] ICreateResourceHandler createResourceHandler,
        [FromServices] IValidator<CreateResourceCommand> createResourceValidator,
        CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var userRole = User.GetRole();

        var validationResult =
            createResourceValidator.Validate(command);

        if (validationResult.Count > 0)
        {
            return BadRequest(
                ApiResponse<ResourceDto>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    validationResult));
        }

        var resourceDto = await createResourceHandler.HandleAsync(
            command,
            userId,
            userRole,
            cancellationToken);

        return Ok(
            ApiResponse<ResourceDto>.Ok(resourceDto));
    }

    /// <summary>
    /// Updates an existing resource.
    /// </summary>
    /// <param name="resourceId">The ID of the resource to update.</param>
    /// <param name="command">The updated resource data.</param>
    /// <returns>The updated resource.</returns>
    [HttpPut("resources/{resourceId:guid}")]
    public async Task<IActionResult> Update(
        Guid resourceId,
        [FromBody] UpdateResourceCommand command,
        [FromServices] IUpdateResourceHandler updateResourceHandler,
        [FromServices] IValidator<UpdateResourceCommand> updateResourceValidator,
        CancellationToken cancellationToken)
    {
        var commandWithId = command with { ResourceId = resourceId };

        var validationResult =
            updateResourceValidator.Validate(commandWithId);

        if (validationResult.Count > 0)
        {
            return BadRequest(
                ApiResponse<ResourceDto>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    validationResult));
        }

        var userId = User.GetUserId();
        var roleCode = User.GetRole();

        var updatedResourceDto = await updateResourceHandler.HandleAsync(
            commandWithId,
            userId,
            roleCode,
            cancellationToken);

        return Ok(
            ApiResponse<ResourceDto>.Ok(updatedResourceDto));
    }


    [HttpGet("resources/types")]
    public IActionResult GetResourceTypes()
    {
        var resourceTypes = Enum
            .GetValues<ResourceType>()
            .Where(resourceType => resourceType != ResourceType.None)
            .Select(resourceType => new
            {
                value = (int)resourceType,
                name = resourceType.ToString()
            })
            .ToList();

        return Ok(ApiResponse<object>.Ok(resourceTypes));
    }

    [HttpGet("activities/{activityId:guid}/submission")]
    public async Task<IActionResult> GetMySubmission(
        Guid activityId,
        [FromServices] IGetMySubmissionHandler getMySubmissionHandler,
        CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();

        var submission =
            await getMySubmissionHandler.HandleAsync(
                new GetMySubmissionQuery(
                    activityId,
                    userId),
                cancellationToken);

        return Ok(
            ApiResponse<ResourceDto?>.Ok(submission));
    }
}
