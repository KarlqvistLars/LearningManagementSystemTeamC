namespace LearningManagementSystemTeamC.Application.Courses.Queries.GetCoursesByIdRange;

public record GetCoursesByIdRangeQuery(IEnumerable<Guid> CourseIds);