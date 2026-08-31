using LearningManagementSystemTeamC.Application.Courses;
using LearningManagementSystemTeamC.Domain.Courses;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
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