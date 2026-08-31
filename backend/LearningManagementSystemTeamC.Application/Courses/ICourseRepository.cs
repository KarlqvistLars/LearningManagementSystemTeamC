using LearningManagementSystemTeamC.Domain.Courses;

namespace LearningManagementSystemTeamC.Application.Courses;

public interface ICourseRepository
{
    Task AddAsync(Course course, CancellationToken cancellationToken);
}
