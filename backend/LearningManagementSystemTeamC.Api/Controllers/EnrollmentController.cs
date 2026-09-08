using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Enrollments.Commands.CreateEnrollment;
using LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollments;
using LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsById;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/enrollments")]
public class EnrollmentController : ControllerBase
{
    public EnrollmentController() { }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] IGetEnrollmentsHandler getEnrollmentsHandler, CancellationToken cancellationToken)
    {
        var enrollments = await getEnrollmentsHandler.Handle(cancellationToken);
        return Ok(ApiResponse<IEnumerable<EnrollmentDto>>.Ok(enrollments));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, [FromServices] GetEnrollmentsByIdHandler getEnrollmentByIdHandler, CancellationToken cancellationToken)
    {
        var enrollment = await getEnrollmentByIdHandler.Handle(new GetEnrollmentsByIdQuery(id), cancellationToken);
        if (enrollment == null)
        {
            return NotFound(
                ApiResponse<EnrollmentDto>.Fail(
                    ExceptionConstants.NotFoundCode,
                    ExceptionConstants.NotFoundMessage));
        }
        return Ok(ApiResponse<EnrollmentDto>.Ok(enrollment));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateEnrollmentCommand command,
        [FromServices] ICreateEnrollmentHandler createEnrollmentHandler,
        [FromServices] IValidator<CreateEnrollmentCommand> createEnrollmentValidator,
        CancellationToken cancellationToken)
    {
        var details = createEnrollmentValidator.Validate(command);
        if (details.Count > 0)
        {
            return BadRequest(
                ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));
        }
        var createdEnrollment = await createEnrollmentHandler.Handle(
            command,
            cancellationToken);
        return CreatedAtAction(
            nameof(GetAll),
            new { id = createdEnrollment.Id },
            ApiResponse<EnrollmentDto>.Ok(createdEnrollment));
    }
}
