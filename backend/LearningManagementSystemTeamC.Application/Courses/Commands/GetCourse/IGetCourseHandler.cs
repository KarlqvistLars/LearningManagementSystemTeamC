using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Courses.Commands.GetCourse;

public interface IGetCourseHandler
{
    Task<CourseDto?> Handle(Guid id, CancellationToken cancellationToken);
}
