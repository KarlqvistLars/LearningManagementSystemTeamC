using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Courses;
using LearningManagementSystemTeamC.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using LearningManagementSystemTeamC.Application.Common.Mappers;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CourseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Courses
            .Select(CourseMapper.CourseToDto)
            .ToListAsync(cancellationToken);
    }

    public async Task<CourseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Courses
            .Where(course => course.Id == id)
            .Select(CourseMapper.CourseToDto)
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