using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourses;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCoursesByIdRange;
using LearningManagementSystemTeamC.Application.Enrollments.Commands.EnrollUserInCourse;
using LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByCourseId;
using LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByUserId;
using LearningManagementSystemTeamC.Domain.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Authorize(Policy = PolicyConstants.AuthenticatedUser)]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    public CoursesController() { }

    [HttpGet]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    public async Task<IActionResult> GetAll([FromServices] IGetCoursesHandler getCoursesHandler, CancellationToken cancellationToken)
    {
        var courses = await getCoursesHandler.Handle(cancellationToken);
        return Ok(ApiResponse<IEnumerable<CourseDto>>.Ok(courses));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, [FromServices] IGetCourseByIdHandler getCourseHandler, CancellationToken cancellationToken)
    {
        var course = await getCourseHandler.Handle(new GetCourseByIdQuery(id), cancellationToken);
        if (course == null)
        {
            return NotFound(ApiResponse<CourseDto>.Fail(ExceptionConstants.NotFoundCode, ExceptionConstants.NotFoundMessage));
        }
        return Ok(ApiResponse<CourseDto>.Ok(course));
    }

    [HttpPost]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    public async Task<IActionResult> Create(
        CreateCourseCommand command,
        [FromServices] ICreateCourseHandler createCourseHandler,
        [FromServices] IValidator<CreateCourseCommand> createCourseValidator,
        CancellationToken cancellationToken)
    {
        var details = createCourseValidator.Validate(command);

        if (details.Count > 0)
        {
            return BadRequest(
                ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));
        }

        var courseDto = await createCourseHandler.Handle(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = courseDto.Id },
            ApiResponse<CourseDto>.Ok(courseDto));
    }

    [HttpGet("~/api/student/{userId}/courses")]
    public async Task<IActionResult> GetCoursesByUserId(Guid userId, [FromServices] IGetEnrollmentsByUserIdHandler getEnrollmentsByUserIdHandler, [FromServices] IGetCoursesByIdRangeHandler getCoursesByIdRangeHandler, CancellationToken cancellationToken)
    {
        var enrollments = await getEnrollmentsByUserIdHandler.Handle(new GetEnrollmentsByUserIdQuery(userId), cancellationToken);
        if (!enrollments.Any())
        {
            return NotFound(ApiResponse<IEnumerable<CourseDto>>.Fail(ExceptionConstants.NotFoundCode, ExceptionConstants.NotFoundMessage));
        }

        var courseIds = enrollments.Select(e => e.CourseId);
        var courses = await getCoursesByIdRangeHandler.Handle(new GetCoursesByIdRangeQuery(courseIds), cancellationToken);
        return Ok(ApiResponse<IEnumerable<CourseDto>>.Ok(courses));
    }

    [HttpGet("{courseId}/enrollments")]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    public async Task<IActionResult> GetEnrollmentsByCourseId(Guid courseId, [FromServices] IGetEnrollmentsByCourseIdHandler getEnrollmentsByCourseIdHandler, CancellationToken cancellationToken)
    {
        var enrollments = await getEnrollmentsByCourseIdHandler.Handle(new GetEnrollmentsByCourseIdQuery(courseId), cancellationToken);
        if (!enrollments.Any())
        {
            return NotFound(ApiResponse<IEnumerable<CourseEnrollmentDto>>.Fail(ExceptionConstants.NotFoundCode, ExceptionConstants.NotFoundMessage));
        }
        return Ok(ApiResponse<IEnumerable<CourseEnrollmentDto>>.Ok(enrollments));
    }

    [HttpPost("{courseId}/enroll/{userId}")]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    public async Task<IActionResult> EnrollUserInCourse(Guid courseId, Guid userId, [FromServices] IEnrollUserInCourseHandler enrollUserInCourseHandler, CancellationToken cancellationToken)
    {
        var enrollment = await enrollUserInCourseHandler.Handle(new EnrollUserInCourseCommand(userId, courseId), cancellationToken);
        return enrollment ? Ok(ApiResponse<bool>.Ok(enrollment)) 
            : BadRequest(ApiResponse<bool>.Fail(ExceptionConstants.DefaultExceptionCode, ExceptionConstants.DefaultExceptionMessage));
    }
}