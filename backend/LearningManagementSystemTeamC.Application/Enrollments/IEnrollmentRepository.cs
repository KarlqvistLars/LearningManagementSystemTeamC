using LearningManagementSystemTeamC.Domain.Enrollments;

namespace LearningManagementSystemTeamC.Application.Enrollments;

public interface IEnrollmentRepository
{
    Task AddAsync(Enrollment enrollment, CancellationToken cancellationToken);
    Task DeleteAsync(Enrollment enrollment,  CancellationToken cancellationToken);
    Task<IEnumerable<Enrollment>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<Enrollment>> GetByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);
}
