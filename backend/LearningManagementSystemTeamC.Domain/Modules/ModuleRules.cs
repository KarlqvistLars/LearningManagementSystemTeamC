namespace LearningManagementSystemTeamC.Domain.Modules;

public static class ModuleRules
{
    public const int ModuleNameMaxLength = 200;
    public const int DescriptionMaxLength = 2000;


    public const string ModuleNameRequiredCode = "  MODULE_NAME_REQUIRED";
    public const string ModuleNameRequiredMessage = "Module name is required. ";
    public const string ModuleNameToLongCode = "MODULE_NAME_TOO_LONG";
    public const string ModuleNameToLongMessage = "Module name cannot exceed 200 characters.";
    public const string ModuleDescriptionRequiredCode = "DESCRIPTION_REQUIRED";
    public const string ModuleDescriptionRequiredMessage = "Description is required.";
    public const string ModuleDescriptionTooLongCode = "MODULE_DESCRIPTION_TOO_LONG";
    public const string ModuleDescriptionTooLongMessage = "Module description cannot exceed 2000 characters.";
    public const string InvalidModuleDateCode = "ENDDATE_AFTER_STARTDATE";
    public const string InvalidModuleDateMessage =  "End date must be after start date.";
    public const string CourseIdRequiredCode = "COURSE_ID_REQUIRED";
    public const string CourseIdRequiredMessage = "Course ID is required.";
    public const string InvalidCourseIdCode ="COURSE_ID_INVALID";
    public const string InvalidCourseIdMessage = "Course ID is invalid.";

}