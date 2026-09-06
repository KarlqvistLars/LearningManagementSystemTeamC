using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;

namespace LearningManagementSystemTeamC.Application.Courses.Queries.GetCourse;

public class GetCourseByIdHandler : IGetCourseByIdHandler
{
    private readonly ICourseRepository _courseRepository;

    public GetCourseByIdHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<CourseDto?> Handle(GetCourseByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _courseRepository.GetByIdAsync(query.Id, cancellationToken);
        return result == null ? null : CourseMapper.CourseToDto(result);
    }
}