using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Enrollments.Commands.CreateEnrollment;

public interface ICreateEnrollmentHandler
{
    Task<EnrollmentDto> Handle(CreateEnrollmentCommand command, CancellationToken cancellationToken);
}