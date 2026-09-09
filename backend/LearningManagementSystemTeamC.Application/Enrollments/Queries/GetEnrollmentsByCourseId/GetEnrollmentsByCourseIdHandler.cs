using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Courses;
using LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByCourseId;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Courses;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByCourseId;

public class GetEnrollmentsByCourseIdHandler : IGetEnrollmentsByCourseIdHandler
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICourseRepository _courseRepository;

    public GetEnrollmentsByCourseIdHandler(
        IEnrollmentRepository enrollmentRepository,
        IUserRepository userRepository,
        ICourseRepository courseRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _userRepository = userRepository;
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<CourseEnrollmentDto>> Handle(GetEnrollmentsByCourseIdQuery query, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(query.CourseId, cancellationToken);
        if (course == null) 
        {
            throw new NotFoundException(
                CourseRules.CourseNotFoundCode,
                CourseRules.CourseNotFoundMessage);
        }

        var result = await _enrollmentRepository.GetByCourseIdAsync(query.CourseId, cancellationToken);
        var enrolledStudents = result.Select(e => e.UserId).ToList();
        var users = await _userRepository.GetUsersByIdsAsync(enrolledStudents, cancellationToken);
        // Change to user name when UserInfo is implemented
        return result.Select(e => EnrollmentMapper.CourseEnrollmentToDto(e, users.FirstOrDefault(u => u.Id == e.UserId)?.Email ?? string.Empty));
    }
}