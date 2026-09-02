using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourses;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController : ControllerBase
{
    public CoursesController() { }

    [HttpGet]
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
}