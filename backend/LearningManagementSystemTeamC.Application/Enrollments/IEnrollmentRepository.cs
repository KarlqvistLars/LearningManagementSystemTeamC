using LearningManagementSystemTeamC.Domain.Enrollments;

namespace LearningManagementSystemTeamC.Application.Enrollments;

public interface IEnrollmentRepository
{
    Task AddAsync(Enrollment enrollment, CancellationToken cancellationToken);
    Task<IEnumerable<Enrollment>> GetAllAsync(CancellationToken cancellationToken);
    Task<Enrollment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
