using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.Resources;

namespace LearningManagementSystemTeamC.Application.Resources.Command.UpdateResource;

public class UpdateResourceValidator : IValidator<UpdateResourceCommand>
{
    public Dictionary<string, string[]> Validate(UpdateResourceCommand command, CancellationToken cancellationToken)
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

        if (command.Type == ResourceType.None)
        {
            errors[nameof(command.Type)] = new[] { "Type is required." };
        }

        return errors;
    }
}
