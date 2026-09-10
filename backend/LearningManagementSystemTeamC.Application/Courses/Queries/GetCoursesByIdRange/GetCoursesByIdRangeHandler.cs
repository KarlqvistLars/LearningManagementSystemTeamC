using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;

namespace LearningManagementSystemTeamC.Application.Courses.Queries.GetCoursesByIdRange;

public class GetCoursesByIdRangeHandler : IGetCoursesByIdRangeHandler
{
    private readonly ICourseRepository _courseRepository;

    public GetCoursesByIdRangeHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<CourseDto>> Handle(GetCoursesByIdRangeQuery query, CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetCoursesByIdsAsync(query.CourseIds, cancellationToken);
        return courses.Select(c => CourseMapper.CourseToDto(c));
    }
}