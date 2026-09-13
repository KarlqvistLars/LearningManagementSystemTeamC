using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Application.Modules.Commands.CreateModule;

public class CreateModuleValidator : IValidator<CreateModuleCommand>
{
    public Dictionary<string, string[]> Validate(CreateModuleCommand command)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            errors[nameof(command.Name)] = 
            [
                ModuleRules.ModuleNameRequiredMessage
            ];
        }
        else if (command.Name.Length > ModuleRules.ModuleNameMaxLength)
        {
            errors[nameof(command.Name)] =
            [
                ModuleRules.ModuleNameToLongMessage
            ];
        }
        
        if (string.IsNullOrWhiteSpace(command.Description))
        {
            errors[nameof(command.Description)] =
            [
                ModuleRules.ModuleDescriptionRequiredMessage  
            ];
        }
        else if (command.Description.Length > ModuleRules.DescriptionMaxLength)
        {
            errors[nameof(command.Description)] =
            [
                ModuleRules.ModuleDescriptionTooLongMessage  
            ];
        }

        if (command.EndDate <= command.StartDate)
        {
            errors[nameof(command.EndDate)] =
            [
                ModuleRules.InvalidModuleDateMessage  
            ];
        }

        return errors;
    }
}