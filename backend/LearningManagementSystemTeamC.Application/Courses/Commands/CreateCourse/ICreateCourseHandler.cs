using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;

public interface ICreateCourseHandler
{
    Task<CourseDto> Handle(CreateCourseCommand command, CancellationToken cancellationToken);
}
