using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Courses.Queries.GetCoursesByIdRange;

public interface IGetCoursesByIdRangeHandler
{
    Task<IEnumerable<CourseDto>> Handle(GetCoursesByIdRangeQuery query, CancellationToken cancellationToken);
}