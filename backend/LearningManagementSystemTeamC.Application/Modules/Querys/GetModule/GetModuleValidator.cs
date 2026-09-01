using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Application.Modules.Query.GetModule;

public class GetModuleValidator
{
    public Dictionary<string, string[]> Validate(
        GetModuleQuery query)
    {
        var errors = new Dictionary<string, string[]>();

        if (query.CourseId == Guid.Empty)
        {
            errors[nameof(query.CourseId)] =
            [
                ModuleRules.CourseIdRequiredMessage
            ];
        }
        
        return errors;
    }
}