using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;

namespace LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsById;

public class GetEnrollmentsByIdHandler : IGetEnrollmentsByIdHandler
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    public GetEnrollmentsByIdHandler(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }
    public async Task<EnrollmentDto?> Handle(GetEnrollmentsByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _enrollmentRepository.GetByIdAsync(query.Id, cancellationToken);
        return result == null ? null : EnrollmentMapper.EnrollmentToDto(result);
    }
}
