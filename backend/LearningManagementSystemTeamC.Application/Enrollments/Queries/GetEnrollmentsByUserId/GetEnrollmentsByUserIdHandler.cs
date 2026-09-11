using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Courses;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByUserId;

public class GetEnrollmentsByUserIdHandler : IGetEnrollmentsByUserIdHandler
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;

    public GetEnrollmentsByUserIdHandler(
        IEnrollmentRepository enrollmentRepository,
        ICourseRepository courseRepository,
        IUserRepository userRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseRepository = courseRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<EnrollmentDto>> Handle(GetEnrollmentsByUserIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException(
                UserRules.UserNotFoundCode,
                UserRules.UserNotFoundMessage);
        }
        else if (!user.IsActive)
        {
            throw new NotFoundException(
                UserRules.AccountNotAvailableCode,
                UserRules.AccountNotAvailableMessage);
        }

        var enrollments = await _enrollmentRepository.GetByUserIdAsync(query.UserId, cancellationToken);
        return enrollments.Select(e => EnrollmentMapper.EnrollmentToDto(e));
    }
}