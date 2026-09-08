using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;

namespace LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollments;

public class GetEnrollmentsHandler : IGetEnrollmentsHandler
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public GetEnrollmentsHandler(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<IEnumerable<EnrollmentDto>> Handle(CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentRepository.GetAllAsync(cancellationToken);
        return enrollments.Any() ? enrollments.Select(enrollment => EnrollmentMapper.EnrollmentToDto(enrollment)) : Enumerable.Empty<EnrollmentDto>();
    }
}
