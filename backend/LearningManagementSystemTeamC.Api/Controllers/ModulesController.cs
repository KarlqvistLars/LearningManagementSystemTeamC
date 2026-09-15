using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Modules.Commands.CreateModule;
using LearningManagementSystemTeamC.Application.Modules.Commands.EditModule;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModuleById;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Authorize(Policy = PolicyConstants.AuthenticatedUser)]
[Route("api/modules")]
public class ModulesController : ControllerBase
{
    public ModulesController() { }

    /// <summary>
    /// Gets a module by its ID.
    /// </summary>
    /// <param name="id">The ID of the module.</param>
    /// <returns>The requested module.</returns>
    [HttpGet("{id:guid}")]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    public async Task<ActionResult> GetById(
        Guid id,
        [FromServices] IGetModuleByIdHandler getModuleByIdHandler,
        CancellationToken cancellationToken)
    {
        var module = await getModuleByIdHandler.Handle(
            new GetModuleByIdQuery(id),
            cancellationToken);

        if (module == null)
        {
            return NotFound(
                ApiResponse<ModuleDto>.Fail(
                    ExceptionConstants.NotFoundCode,
                    ExceptionConstants.NotFoundMessage));
        }

        return Ok(
            ApiResponse<ModuleDto>.Ok(module));
    }

    /// <summary>
    /// Gets all modules belonging to a course.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <returns>A list of modules belonging to the specified course.</returns>
    [HttpGet("course/{courseId:guid}")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ModuleDto>>>> GetModuleByCourseId(
        Guid courseId,
        [FromServices] IGetModulesHandler getModuleHandler,
        CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var roleCode = User.GetRole();

        var modules = await getModuleHandler.Handle(
            new GetModulesQuery(courseId, userId, roleCode),
            cancellationToken);

        if (modules.Count == 0)
        {
            return NotFound(
                ApiResponse<ModuleDto>.Fail(
                    ExceptionConstants.NotFoundCode,
                    ExceptionConstants.NotFoundMessage));
        }

        return ApiResponse<IReadOnlyList<ModuleDto>>.Ok(modules);
    }

    /// <summary>
    /// Creates a new module.
    /// </summary>
    /// <param name="command">The module creation data.</param>
    /// <returns>The newly created module.</returns>
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateModuleCommand command,
        [FromServices] ICreateModuleHandler createModuleHandler,
        [FromServices] IValidator<CreateModuleCommand> createModuleValidator,
        CancellationToken cancellationToken)
    {
        var details = createModuleValidator.Validate(command);

        if (details.Count > 0)
        {
            return BadRequest(
                ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.DefaultExceptionMessage,
                    details));
        }

        var moduleDto = await createModuleHandler.Handle(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = moduleDto.Id },
            ApiResponse<ModuleDto>.Ok(moduleDto));
    }

    /// <summary>
    /// Updates an existing module.
    /// </summary>
    /// <param name="command">The updated module data.</param>
    /// <returns>The updated module.</returns>
    [HttpPut]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    public async Task<IActionResult> Edit(
        EditModuleCommand command,
        [FromServices] IEditModuleHandler editModuleHandler,
        [FromServices] IValidator<EditModuleCommand> editModuleValidator,
        CancellationToken cancellationToken)
    {
        var details = editModuleValidator.Validate(command);

        if (details.Count > 0)
        {
            return BadRequest(
                ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.DefaultExceptionMessage,
                    details));
        }

        var moduleDto = await editModuleHandler.Handle(
            command,
            cancellationToken);

        return Ok(
            ApiResponse<ModuleDto>.Ok(moduleDto));
    }
}