using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemTeamC.Api.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    private readonly CreateCourseHandler _createCourseHandler;
    private readonly CreateCourseValidator _createCourseValidator;

    public CoursesController(CreateCourseHandler createCourseHandler, CreateCourseValidator createCourseValidator)
    {
        _createCourseHandler = createCourseHandler;
        _createCourseValidator = createCourseValidator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCourseCommand command,
        CancellationToken cancellationToken)
    {
        // If not using other tools:
        // Run validator
        var isValid = _createCourseValidator.IsValid(command);

        if (!isValid)
        {
            // No magic string, const instead
            return BadRequest("Validation failed");
        }

        var courseDto = await _createCourseHandler.Handle(
            command,
            cancellationToken);

        // Should be GetById instead of Create
        return CreatedAtAction(
            nameof(Create),
            new { id = courseDto.Id },
            ApiResponse<CourseDto>.Ok(courseDto));
    }
}