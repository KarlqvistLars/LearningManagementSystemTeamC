using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByUserId;

public interface IGetEnrollmentsByUserIdHandler
{
    Task<IEnumerable<CourseDto>> Handle(GetEnrollmentsByUserIdQuery query, CancellationToken cancellationToken);
}