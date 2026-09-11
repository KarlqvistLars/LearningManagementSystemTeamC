using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Courses;

namespace LearningManagementSystemTeamC.Application.Courses.Commands.UpdateCourse;

public class UpdateCourseHandler : IUpdateCourseHandler
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCourseHandler(
        ICourseRepository courseRepository,
        IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CourseDto> Handle(
        UpdateCourseCommand command,
        CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(command.Id, cancellationToken) ??
            throw new NotFoundException(
                CourseRules.CourseNotFoundCode, 
                CourseRules.CourseNotFoundMessage);

        course.Update(command.Name, command.Description, command.StartDate, command.EndDate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CourseMapper.CourseToDto(course);
    }
}