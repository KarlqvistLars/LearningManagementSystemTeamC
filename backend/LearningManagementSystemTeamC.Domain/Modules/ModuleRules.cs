namespace LearningManagementSystemTeamC.Domain.Modules;

public static class ModuleRules
{
    public const int ModuleNameMaxLength = 200;
    public const int DescriptionMaxLength = 2000;

    public const string ModuleNameRequiredCode = "Course name cannot be empty.";
    public const string ModuleDescriptionRequiredCode = "Description cannot be empty.";
    public const string ModuleEndBeforeStartDateCode =  "End date must be after start date.";
    public const string CourseIdRequiredMessage = "Course ID cannot be empty.";

}