using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Modules.Commands.CreateModule;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModule;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/modules")]
public class ModulesController : ControllerBase
{

    public ModulesController() { }

    [HttpGet("{courseId}")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ModuleDto>>>> GetModuleByCourseId(Guid courseId, 
    [FromServices]IGetModuleHandler getModuleHandler,
    CancellationToken cancellationToken)
    {
        var modules = await getModuleHandler.Handle(new GetModuleQuery(courseId), cancellationToken);
        if (modules.Count == 0)
        {
            return NotFound(ApiResponse<ModuleDto>.Fail(ExceptionConstants.NotFoundCode, ExceptionConstants.NotFoundMessage));
        }

        return ApiResponse<IReadOnlyList<ModuleDto>>.Ok(modules);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateModuleCommand command,
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

        var moduleDto = await createModuleHandler.Handle(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetModuleByCourseId),
            new { courseId = command.CourseId },
            ApiResponse<ModuleDto>.Ok(moduleDto));
    }
}