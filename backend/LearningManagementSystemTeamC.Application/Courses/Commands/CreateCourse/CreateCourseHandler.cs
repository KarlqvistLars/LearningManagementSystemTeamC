using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.Courses;

namespace LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;

public class CreateCourseHandler : ICreateCourseHandler
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseHandler(
        ICourseRepository courseRepository,
        IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CourseDto> Handle(
        CreateCourseCommand command,
        CancellationToken cancellationToken)
    {
        // Validation if not using other tools

        // Entity's method should have validation inside
        var course = new Course(
            command.Name,
            command.Description,
            command.StartDate,
            command.EndDate);

        // featureRepository handles actions
        await _courseRepository.AddAsync(
            course,
            cancellationToken);

        // UnitOfWork handles save
        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return new CourseDto(course.Id, course.CourseName, course.Description, course.StartDate, course.EndDate);
    }
}