using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Courses.Commands.UpdateCourse;

public interface IUpdateCourseHandler
{
    Task<CourseDto> Handle(UpdateCourseCommand command, CancellationToken cancellationToken);
}
