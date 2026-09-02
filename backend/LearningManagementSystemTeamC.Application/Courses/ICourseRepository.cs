using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.Courses;

namespace LearningManagementSystemTeamC.Application.Courses;

public interface ICourseRepository
{
    Task AddAsync(Course course, CancellationToken cancellationToken);
    Task<IEnumerable<Course>> GetAllAsync(CancellationToken cancellationToken);
    Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
