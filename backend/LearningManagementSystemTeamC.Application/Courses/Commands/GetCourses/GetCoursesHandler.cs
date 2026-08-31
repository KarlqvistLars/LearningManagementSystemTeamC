using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Courses.Commands.GetCourses;

public class GetCoursesHandler
{
    private readonly ICourseRepository _courseRepository;

    public GetCoursesHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<CourseDto>> Handle(CancellationToken cancellationToken)
    {
        return await _courseRepository.GetAllAsync(cancellationToken);
    }
}