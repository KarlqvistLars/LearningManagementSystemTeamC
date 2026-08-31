using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Courses.Commands.GetCourse;

public class GetCourseHandler
{
    private readonly ICourseRepository _courseRepository;

    public GetCourseHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<CourseDto?> Handle(Guid id, CancellationToken cancellationToken)
    {
        return await _courseRepository.GetByIdAsync(id, cancellationToken);
    }
}