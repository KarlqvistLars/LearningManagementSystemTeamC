using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Courses.Commands.GetCourses;

public interface IGetCoursesHandler
{
    Task<IEnumerable<CourseDto>> Handle(CancellationToken cancellationToken);
}
