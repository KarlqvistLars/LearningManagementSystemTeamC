using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Courses;

namespace LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByUserId;

public class GetEnrollmentsByUserIdHandler : IGetEnrollmentsByUserIdHandler
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseRepository _courseRepository;

    public GetEnrollmentsByUserIdHandler(
        IEnrollmentRepository enrollmentRepository,
        ICourseRepository courseRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<CourseDto>> Handle(GetEnrollmentsByUserIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _enrollmentRepository.GetByUserIdAsync(query.UserId, cancellationToken);
        var enrolledCourses = result.Select(e => e.CourseId).ToList();
        var courses = await _courseRepository.GetCoursesByIdsAsync(enrolledCourses, cancellationToken);
        return courses.Select(c => CourseMapper.CourseToDto(c));
    }
}