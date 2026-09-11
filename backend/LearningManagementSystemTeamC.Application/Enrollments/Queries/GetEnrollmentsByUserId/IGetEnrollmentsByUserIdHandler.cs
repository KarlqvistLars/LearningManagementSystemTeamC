using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByUserId;

public interface IGetEnrollmentsByUserIdHandler
{
    Task<IEnumerable<EnrollmentDto>> Handle(GetEnrollmentsByUserIdQuery query, CancellationToken cancellationToken);
}