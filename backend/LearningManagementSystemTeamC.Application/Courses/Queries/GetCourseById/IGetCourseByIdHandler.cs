using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Courses.Queries.GetCourse;

public interface IGetCourseByIdHandler
{
    Task<CourseDto?> Handle(GetCourseByIdQuery query, CancellationToken cancellationToken);
}
