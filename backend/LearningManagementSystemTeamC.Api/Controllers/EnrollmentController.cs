using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/enrollments")]
public class EnrollmentController : ControllerBase
{
    public EnrollmentController()
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] IEnrollmentService enrollmentService, CancellationToken cancellationToken)
    {
        var enrollments = await enrollmentService.GetAllEnrollmentsAsync(cancellationToken);
        return Ok();
    }
}
