using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
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
        var course = new Course(
            command.Name,
            command.Description,
            command.StartDate,
            command.EndDate);

        await _courseRepository.AddAsync(
            course,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return CourseMapper.CourseToDto(course);
    }
}