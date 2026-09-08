using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsById;

internal interface IGetEnrollmentsByIdHandler
{
    Task<EnrollmentDto?> Handle(GetEnrollmentsByIdQuery query, CancellationToken cancellationToken);
}
