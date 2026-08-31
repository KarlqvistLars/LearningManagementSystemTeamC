using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
using LearningManagementSystemTeamC.Application.Courses.Commands.GetCourse;
using LearningManagementSystemTeamC.Application.Courses.Commands.GetCourses;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    private readonly CreateCourseHandler _createCourseHandler;
    private readonly GetCoursesHandler _getCoursesHandler;
    private readonly GetCourseHandler _getCourseHandler;
    private readonly CreateCourseValidator _createCourseValidator;

    public CoursesController(
        CreateCourseHandler createCourseHandler,
        CreateCourseValidator createCourseValidator,
        GetCoursesHandler getCoursesHandler,
        GetCourseHandler getCourseHandler)
    {
        _createCourseHandler = createCourseHandler;
        _createCourseValidator = createCourseValidator;
        _getCoursesHandler = getCoursesHandler;
        _getCourseHandler = getCourseHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var courses = await _getCoursesHandler.Handle(cancellationToken);
        return Ok(ApiResponse<IEnumerable<CourseDto>>.Ok(courses));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var course = await _getCourseHandler.Handle(id, cancellationToken);
        if (course == null)
        {
            return NotFound(ApiResponse<CourseDto>.Fail(ExceptionConstants.NotFoundCode, ExceptionConstants.NotFoundMessage));
        }
        return Ok(ApiResponse<CourseDto>.Ok(course));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCourseCommand command,
        CancellationToken cancellationToken)
    {
        var details = _createCourseValidator.Validate(command);

        if (details.Count > 0)
        {
            return BadRequest(
                ApiResponse<Dictionary<string, string[]>>.Fail(
                    ExceptionConstants.ValidationFailedCode,
                    ExceptionConstants.ValidationFailedMessage,
                    details));
        }

        var courseDto = await _createCourseHandler.Handle(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = courseDto.Id },
            ApiResponse<CourseDto>.Ok(courseDto));
    }
}