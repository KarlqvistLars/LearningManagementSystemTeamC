using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.Enrollments;

namespace LearningManagementSystemTeamC.Application.Enrollments.Commands.CreateEnrollment;

public class CreateEnrollmentValidator : IValidator<CreateEnrollmentCommand>
{
    public Dictionary<string, string[]> Validate(
        CreateEnrollmentCommand command)
    {
        var errors = new Dictionary<string, string[]>();

        if (command.UserId == Guid.Empty)
        {
            errors[nameof(command.UserId)] =
            [
                EnrollmentRules.UserIdRequiredMessage
            ];
        }
        return errors;
    }
}
