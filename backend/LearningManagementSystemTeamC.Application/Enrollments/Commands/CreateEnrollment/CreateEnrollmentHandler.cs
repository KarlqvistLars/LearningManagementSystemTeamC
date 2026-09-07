using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.Enrollments;

namespace LearningManagementSystemTeamC.Application.Enrollments.Commands.CreateEnrollment;

public class CreateEnrollmentHandler : ICreateEnrollmentHandler
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateEnrollmentHandler(
        IEnrollmentRepository enrollmentRepository,
        IUnitOfWork unitOfWork)
    {
        _enrollmentRepository = enrollmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<EnrollmentDto> Handle(CreateEnrollmentCommand command,
        CancellationToken cancellationToken)
    {
        var enrollment = new Enrollment(
            command.UserId,
            command.CourseId);

        await _enrollmentRepository.AddAsync(enrollment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new EnrollmentDto {
            Id = enrollment.Id,
            UserId = enrollment.UserId,
            CourseId = enrollment.CourseId,
            EnrolledAt = enrollment.EnrolledAt,
            IsActive = enrollment.IsActive
        };
    }
}
