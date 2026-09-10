using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByCourseId;

public interface IGetEnrollmentsByCourseIdHandler
{
    Task<IEnumerable<CourseEnrollmentDto>> Handle(GetEnrollmentsByCourseIdQuery query, CancellationToken cancellationToken);
}
