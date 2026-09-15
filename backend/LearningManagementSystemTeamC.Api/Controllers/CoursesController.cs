using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
using LearningManagementSystemTeamC.Application.Courses.Commands.UpdateCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourses;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCoursesByIdRange;
using LearningManagementSystemTeamC.Application.Enrollments.Commands.EnrollUserInCourse;
using LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByCourseId;
using LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByUserId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Authorize(Policy = PolicyConstants.AuthenticatedUser)]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    public CoursesController() { }

    /// <summary>
    /// Gets all courses.
    /// </summary>
    /// <returns>A list of all courses.</returns>
    [HttpGet]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    public async Task<IActionResult> GetAll(
        [FromServices] IGetCoursesHandler getCoursesHandler,
        CancellationToken cancellationToken)
    {
        var courses = await getCoursesHandler.Handle(cancellationToken);

        return Ok(
            ApiResponse<IEnumerable<CourseDto>>.Ok(courses));
    }

    /// <summary>
    /// Gets a course by its ID.
    /// </summary>
    /// <param name="id">The ID of the course.</param>
    /// <returns>The requested course.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        Guid id,
        [FromServices] IGetCourseByIdHandler getCourseHandler,
        CancellationToken cancellationToken)
    {
        var course = await getCourseHandler.Handle(
            new GetCourseByIdQuery(id),
            cancellationToken);

        if (course == null)
        {
            return NotFound(
                ApiResponse<CourseDto>.Fail(
                    ExceptionConstants.NotFoundCode,
                    ExceptionConstants.NotFoundMessage));
        }

        return Ok(
            ApiResponse<CourseDto>.Ok(course));
    }

    /// <summary>
    /// Creates a new course.
    /// </summary>
    /// <param name="command">The course creation data.</param>
    /// <returns>The newly created course.</returns>
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

    /// <summary>
    /// Updates an existing course.
    /// </summary>
    /// <param name="id">The ID of the course to update.</param>
    /// <param name="command">The updated course data.</param>
    /// <returns>The updated course.</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateCourseCommand command,
        [FromServices] IUpdateCourseHandler updateCourseHandler,
        [FromServices] IValidator<UpdateCourseCommand> updateCourseValidator,
        CancellationToken cancellationToken)
    {
        var commandWithId = command with { Id = id };

        var validationResult = updateCourseValidator.Validate(commandWithId);

        if (validationResult.Count > 0)
        {
            return BadRequest(
                ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.DefaultExceptionMessage,
                    validationResult));
        }

        var courseDto = await updateCourseHandler.Handle(
            commandWithId,
            cancellationToken);

        return Ok(
            ApiResponse<CourseDto>.Ok(courseDto));
    }

    /// <summary>
    /// Gets all courses in which a specific student is enrolled.
    /// </summary>
    /// <param name="userId">The ID of the student.</param>
    /// <returns>A list of courses the student is enrolled in.</returns>
    [HttpGet("~/api/student/{userId}/courses")]
    public async Task<IActionResult> GetCoursesByUserId(
        Guid userId,
        [FromServices] IGetEnrollmentsByUserIdHandler getEnrollmentsByUserIdHandler,
        [FromServices] IGetCoursesByIdRangeHandler getCoursesByIdRangeHandler,
        CancellationToken cancellationToken)
    {
        var enrollments = await getEnrollmentsByUserIdHandler.Handle(
            new GetEnrollmentsByUserIdQuery(userId),
            cancellationToken);

        if (!enrollments.Any())
        {
            return NotFound(
                ApiResponse<IEnumerable<CourseDto>>.Fail(
                    ExceptionConstants.NotFoundCode,
                    ExceptionConstants.NotFoundMessage));
        }

        var courseIds = enrollments.Select(e => e.CourseId);

        var courses = await getCoursesByIdRangeHandler.Handle(
            new GetCoursesByIdRangeQuery(courseIds),
            cancellationToken);

        return Ok(
            ApiResponse<IEnumerable<CourseDto>>.Ok(courses));
    }

    /// <summary>
    /// Gets all student enrollments for a course.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <returns>A list of enrollments for the specified course.</returns>
    [HttpGet("{courseId}/enrollments")]
    public async Task<IActionResult> GetEnrollmentsByCourseId(
        Guid courseId,
        [FromServices] IGetEnrollmentsByCourseIdHandler getEnrollmentsByCourseIdHandler,
        CancellationToken cancellationToken)
    {
        var enrollments = await getEnrollmentsByCourseIdHandler.Handle(
            new GetEnrollmentsByCourseIdQuery(courseId),
            cancellationToken);

        return Ok(
            ApiResponse<IEnumerable<CourseEnrollmentDto>>.Ok(enrollments));
    }

    /// <summary>
    /// Enrolls a user in a course.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="userId">The ID of the user to enroll.</param>
    /// <returns>A confirmation indicating whether the user was enrolled successfully.</returns>
    [HttpPost("{courseId}/enroll/{userId}")]
    [Authorize(Policy = PolicyConstants.TeacherOnly)]
    public async Task<IActionResult> EnrollUserInCourse(
        Guid courseId,
        Guid userId,
        [FromServices] IEnrollUserInCourseHandler enrollUserInCourseHandler,
        CancellationToken cancellationToken)
    {
        var enrollment = await enrollUserInCourseHandler.Handle(
            new EnrollUserInCourseCommand(userId, courseId),
            cancellationToken);

        return enrollment
            ? Ok(ApiResponse<bool>.Ok(enrollment))
            : BadRequest(
                ApiResponse<bool>.Fail(
                    ExceptionConstants.DefaultExceptionCode,
                    ExceptionConstants.DefaultExceptionMessage));
    }
}