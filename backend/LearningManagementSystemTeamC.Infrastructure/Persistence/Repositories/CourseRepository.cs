using LearningManagementSystemTeamC.Application.Courses;
using LearningManagementSystemTeamC.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Courses.ToListAsync(cancellationToken);
    }

    public async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Courses
            .Where(course => course.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(
        Course course,
        CancellationToken cancellationToken)
    {
        await _context.Courses.AddAsync(
            course,
            cancellationToken);
    }
}