using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Authorize(Policy = PolicyConstants.AuthenticatedUser)]
[Route("api/modules")]
public class ModulesController : ControllerBase
{

    public ModulesController() { }

    [HttpGet("{courseId}")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ModuleDto>>>> GetModuleByCourseId(Guid courseId,
    [FromServices]IGetModuleHandler getModuleHandler,
    CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;

        var modules = await getModuleHandler.Handle(new GetModuleQuery(courseId, userId, role), cancellationToken);
        if (modules.Count == 0)
        {
            return NotFound(ApiResponse<ModuleDto>.Fail(ExceptionConstants.NotFoundCode, ExceptionConstants.NotFoundMessage));
        }

        return ApiResponse<IReadOnlyList<ModuleDto>>.Ok(modules);
    }
}