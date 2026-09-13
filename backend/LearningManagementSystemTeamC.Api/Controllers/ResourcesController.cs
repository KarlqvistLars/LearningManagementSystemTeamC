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
    // "api/activities/{activityId}/resources"
    [HttpGet("activities/{activityId}/resources")]
    public async Task<IActionResult> GetByActivityResources(
        Guid activityId,
        [FromServices] IGetResourcesByActivityIdHandler getResourcesByActivityIdHandler,
        CancellationToken cancellationToken)
    {
        // Anropa tabellen för activityResouces (handlern) för att hämta resurser baserat på activityId
        Console.WriteLine($"Fetching resources for activityId: {activityId}");
        var resources = await getResourcesByActivityIdHandler.Handle(
            new GetResourcesByActivityIdQuery(activityId),
            cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ResourceDto>>.Ok(resources));
    }

    // Här vill man ha en POST-metod som skapar en resurs utan koppling till en aktivitet
    // Resursen bör kopplas till en aktivitet i Activity controllern istället.
    // "api/resources"
    [HttpPost("resources")]
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

        // 1. Create the resource
        // 3. Save to the database

        var resourceDto = await createResourceHandler.Handle(
            command,
            cancellationToken);

        return Ok(ApiResponse<ResourceDto>.Ok(resourceDto));
    }
}
