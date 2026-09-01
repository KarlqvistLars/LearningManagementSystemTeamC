using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Modules.Query.GetModule;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/modules")]
public class ModulesController : ControllerBase
{
    private readonly GetModuleHandler _getModuleHandler;
    private readonly GetModuleValidator _getModuleValidator;

    public ModulesController(GetModuleHandler getModuleHandler, GetModuleValidator getModuleValidator)
    {
        _getModuleHandler = getModuleHandler;
        _getModuleValidator = getModuleValidator;
    }

    [HttpGet("{courseId}")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ModuleDto>>>> Get(Guid courseId)
    {
        var query = new GetModuleQuery(courseId);

        var details = _getModuleValidator.Validate(query);

        if (details.Count > 0)
        {
            return BadRequest(
                ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));
        }

        var modulesDto = await _getModuleHandler.Handle(query);

        return ApiResponse<IReadOnlyList<ModuleDto>>.Ok(modulesDto);
    }
}