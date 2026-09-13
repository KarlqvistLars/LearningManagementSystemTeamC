using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.ActivityResources.Command.CreateActivityResource;
using LearningManagementSystemTeamC.Application.ActivityResources.Queries.GetResourcesByActivityId;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Resources.Command.CreateResource;
using LearningManagementSystemTeamC.Application.Resources.Queries.GetAllResources;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api")]
public class ResourcesController : ControllerBase
{
    public ResourcesController() { }

    // Här vill man ha en GET-metod som hämtar alla resurser.
    // api/resources
    [HttpGet("resources")]
    public async Task<IActionResult> GetAllResources(
        [FromServices] IGetAllResourcesHandler getAllResourcesHandler,
        CancellationToken cancellationToken)
    {
        var resources = await getAllResourcesHandler.Handle(
            new GetAllResourcesQuery(Guid.Empty), // Assuming you want to get all resources without filtering by ResourceId
            cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ResourceDto>>.Ok(resources));
    }

    // Här vill man ha en GET-metod som hämtar resurser för en specifik aktivitet
    [HttpGet("activities/{activityId}/resources")]
    public async Task<IActionResult> GetByActivityResources(
        Guid activityId,
        [FromServices] IGetResourcesByActivityIdHandler getResourcesByActivityIdHandler,
        CancellationToken cancellationToken)
    {
        // Anropa tabellen för activityResouces (handlern) för att hämta resurser baserat på activityId
        var resources = await getResourcesByActivityIdHandler.Handle(
            new GetResourcesByActivityIdQuery(activityId),
            cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ResourceDto>>.Ok(resources));
    }

    // Här vill man ha en POST-metod som skapar en resurs och kopplar den till en aktivitet
    [HttpPost("activities/{activityId}/resources")]
    public async Task<IActionResult> Create(
        Guid activityId, [FromServices]
        CreateResourceCommand command,
        [FromServices] ICreateResourceHandler createResourceHandler,
        [FromServices] IValidator<CreateResourceCommand> createResourceValidator,
        CancellationToken cancellationToken)
    {
        var validationResult = createResourceValidator.Validate(command, cancellationToken);
        if (validationResult.Count > 0)
        {
            return BadRequest(
                ApiResponse<ResourceDto>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    validationResult)
                );
        }

        // 1. Create the resource
        // 2. Create the activity-resource association
        // 3. Save both to the database
        // 4. Return the created resource with a link to the activity

        var resourceDto = await createResourceHandler.Handle(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetByActivityResources),
            new { resourceId = resourceDto.Id }, // Assuming you want to return the created resource's ID in the response lägg till kopplingen mellan aktivitet och resurs
            ApiResponse<ResourceDto>.Ok(resourceDto));
    }
}
