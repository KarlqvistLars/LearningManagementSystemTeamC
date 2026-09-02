using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Courses.Queries.GetCourses;

public interface IGetCoursesHandler
{
    Task<IEnumerable<CourseDto>> Handle(CancellationToken cancellationToken);
}
