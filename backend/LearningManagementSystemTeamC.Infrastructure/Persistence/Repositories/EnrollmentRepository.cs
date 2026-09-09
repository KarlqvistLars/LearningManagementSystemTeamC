using LearningManagementSystemTeamC.Application.Enrollments;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Enrollments;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Enrollment enrollment, CancellationToken cancellationToken)
        {
            await _context.Enrollments.AddAsync(enrollment, cancellationToken);
        }

        public void DeleteAsync(Enrollment enrollment, CancellationToken cancellationToken)
        {
            _context.Enrollments.Remove(enrollment);
        }

        public async Task<IEnumerable<Enrollment>> GetByCourseIdAsync(Guid courseId, CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                .Where(e => e.CourseId == courseId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Enrollment>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                .Where(e => e.UserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}
