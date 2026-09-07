using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollments;

public interface IGetEnrollmentsHandler
{
    Task<IEnumerable<EnrollmentDto>> Handle(CancellationToken cancellationToken);
}
