using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;

namespace LearningManagementSystemTeamC.Application.Courses.Queries.GetCourses;

public class GetCoursesHandler : IGetCoursesHandler
{
    private readonly ICourseRepository _courseRepository;

    public GetCoursesHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<CourseDto>> Handle(CancellationToken cancellationToken)
    {
        var result = await _courseRepository.GetAllAsync(cancellationToken);
        return result.Any() ? result.Select(course => CourseMapper.CourseToDto(course)) : Enumerable.Empty<CourseDto>();
    }
}