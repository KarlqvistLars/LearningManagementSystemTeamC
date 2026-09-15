using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Application.Activities.Command.CreateActivity;

public class CreateActivityValidator : IValidator<CreateActivityCommand>
{
    public Dictionary<string, string[]> Validate(CreateActivityCommand command)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.ActivityName))
        {
            errors[nameof(command.ActivityName)] = new[] { ActivityRules.ActivityNameRequiredMessage };
        } else if (command.ActivityName.Length > ActivityRules.ActivityNameMaxLength)
        {
            errors[nameof(command.ActivityName)] = new[] { ActivityRules.ActivityNameTooLongMessage };
        }

        if (string.IsNullOrWhiteSpace(command.Description))
        {
            errors[nameof(command.Description)] = new[] { ActivityRules.ActivityDescriptionRequiredMessage };
        } else if (command.Description.Length > ActivityRules.DescriptionMaxLength)
        {
            errors[nameof(command.Description)] = new[] { ActivityRules.ActivityDescriptionTooLongMessage };
        }

        if (command.EndDate <= command.StartDate)
        {
            errors[nameof(command.EndDate)] = new[] { ActivityRules.ActivityEndBeforeStartTimeMessage };
        }

        return errors;
    }
}
