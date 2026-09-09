using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Courses;
using LearningManagementSystemTeamC.Domain.Enrollments;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Enrollments.Commands.EnrollUserInCourse;

public class EnrollUserInCourseHandler : IEnrollUserInCourseHandler
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EnrollUserInCourseHandler(
        IEnrollmentRepository enrollmentRepository,
        IUserRepository userRepository,
        ICourseRepository courseRepository,
        IUnitOfWork unitOfWork)
    {
        _enrollmentRepository = enrollmentRepository;
        _userRepository = userRepository;
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(EnrollUserInCourseCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken) ??
            throw new NotFoundException(UserRules.UserNotFoundCode, UserRules.UserNotFoundMessage);

        if (!user.IsActive)
            throw new UnauthorizedException(UserRules.AccountNotAvailableCode, UserRules.AccountNotAvailableMessage);

        var course = await _courseRepository.GetByIdAsync(command.CourseId, cancellationToken) ??
            throw new NotFoundException(CourseRules.CourseNotFoundCode, CourseRules.CourseNotFoundMessage);

        var existingEnrollment = await _enrollmentRepository.GetByUserIdAndCourseIdAsync(command.UserId, command.CourseId, cancellationToken);
        if (existingEnrollment != null)
            throw new ConflictException(EnrollmentRules.UserAlreadyEnrolledCode, EnrollmentRules.UserAlreadyEnrolledMessage);

        var enrollment = new Enrollment(command.UserId, command.CourseId);

        await _enrollmentRepository.AddAsync(enrollment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}