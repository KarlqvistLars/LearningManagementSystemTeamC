using LearningManagementSystemTeamC.Application.Common.Interfaces;

namespace LearningManagementSystemTeamC.Application.Activities.CreateActivity;

public class CreateActivityValidator : IValidator<CreateActivityCommand>
{
    public Dictionary<string, string[]> Validate(CreateActivityCommand command, CancellationToken cancellationToken)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.ActivityName))
        {
            errors[nameof(command.ActivityName)] = new[] { "Activity name is required." };
        } else if (command.ActivityName.Length > 100)
        {
            errors[nameof(command.ActivityName)] = new[] { "Activity name is too long." };
        }

        if (string.IsNullOrWhiteSpace(command.Description))
        {
            errors[nameof(command.Description)] = new[] { "Activity description is required." };
        } else if (command.Description.Length > 500)
        {
            errors[nameof(command.Description)] = new[] { "Activity description is too long." };
        }

        if (command.EndDate <= command.StartDate)
        {
            errors[nameof(command.EndDate)] = new[] { "End date must be after start date." };
        }

        return errors;
    }
}
