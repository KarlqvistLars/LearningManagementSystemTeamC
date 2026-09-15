using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.Resources;

namespace LearningManagementSystemTeamC.Application.Resources.Command.CreateResource;

public class CreateResourceValidator : IValidator<CreateResourceCommand>
{
    public Dictionary<string, string[]> Validate(CreateResourceCommand command)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.ResourceName))
        {
            errors[nameof(command.ResourceName)] = new[] { ResourceRules.ResourceNameRequiredMessage };
        }
        else if (command.ResourceName.Length > ResourceRules.ResourceNameMaxLength)
        {
            errors[nameof(command.ResourceName)] = new[] { $"Resource name cannot exceed {ResourceRules.ResourceNameMaxLength} characters." };
        }

        if (string.IsNullOrWhiteSpace(command.Content))
        {
            errors[nameof(command.Content)] = new[] { ResourceRules.ContentRequiredMessage };
        }

        if (command.Type == ResourceType.None)
        {
            errors[nameof(command.Type)] = new[] { ResourceRules.TypeIsRequiredMessage };
        }

        return errors;
    }
}
