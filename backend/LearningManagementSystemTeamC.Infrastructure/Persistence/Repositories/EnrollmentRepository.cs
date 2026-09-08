using LearningManagementSystemTeamC.Application.Enrollments;
using LearningManagementSystemTeamC.Domain.Enrollments;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly ApplicationDbContext _context;
    public EnrollmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Enrollment enrollment, CancellationToken cancellationToken)
    {
        //await _context.Enrollments.AddAsync(enrollment, cancellationToken);
        //await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Enrollment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Enrollments.ToListAsync(cancellationToken);
    }

    public async Task<Enrollment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Enrollments.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }
}
