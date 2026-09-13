using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Resources.Command.CreateResource;

namespace LearningManagementSystemTeamC.Application.ActivityResources.Command.CreateActivityResource;

public class CreateResourceValidator : IValidator<CreateResourceCommand>
{
    public Dictionary<string, string[]> Validate(CreateResourceCommand command, CancellationToken cancellationToken)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.ResourceName))
        {
            errors[nameof(command.ResourceName)] = new[] { "Resource name is required." };
        } else if (command.ResourceName.Length > 100)
        {
            errors[nameof(command.ResourceName)] = new[] { "Resource name is too long." };
        }

        if (string.IsNullOrWhiteSpace(command.Content))
        {
            errors[nameof(command.Content)] = new[] { "Content is required." };
        }

        if (DateTime.UtcNow < command.CreatedAt)
        {
            errors[nameof(command.CreatedAt)] = new[] { "CreatedAt cannot be in the future." };
        }

        if (command.CreatedAt == default)
        {
            errors[nameof(command.CreatedAt)] = new[] { "CreatedAt is required." };
        }

        if (command.Type == default)
        {
            errors[nameof(command.Type)] = new[] { "Type is required." };
        }

        return errors;
    }
}
