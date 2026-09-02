namespace LearningManagementSystemTeamC.Domain.Activities;

public static class ActivityRules
{
    public const int ActivityNameMaxLength = 200;
    public const int DescriptionMaxLength = 2000;

    public const string ActivityNameRequiredCode = "Activity name cannot be empty.";
    public const string ActivityDescriptionRequiredCode = "Description cannot be empty.";
    public const string ActivityTypeRequiredCode = "Type cannot be empty.";
    public const string ActivityEndBeforeStartTimeCode = "End time must be after start time.";
    public const string ModuleIdRequiredMessage = "Module ID cannot be empty.";
}
