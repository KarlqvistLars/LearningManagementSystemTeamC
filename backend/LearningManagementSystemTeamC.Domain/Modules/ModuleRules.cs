namespace LearningManagementSystemTeamC.Domain.Modules;

public static class ModuleRules
{
    public const int ModuleNameMaxLength = 200;
    public const int DescriptionMaxLength = 2000;

    public const string ModuleNameRequiredMessage = "Module name is required. ";
    public const string ModuleDescriptionRequiredMessage = "Description is required.";
    public const string ModuleEndBeforeStartDateMessage =  "End date must be after start date.";
    public const string CourseIdRequiredMessage = "Course ID is required.";
    public const string InvalidCourseIdMessage = "Course ID is invalid.";

}