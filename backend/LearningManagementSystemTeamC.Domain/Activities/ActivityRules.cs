namespace LearningManagementSystemTeamC.Domain.Activities;

public static class ActivityRules
{
    public const int ActivityNameMaxLength = 100;
    public const int DescriptionMaxLength = 500;

    public const string ActivityNameRequiredMessage = "Activity name cannot be empty.";
    public const string ActivityDescriptionRequiredMessage = "Description cannot be empty.";
    public const string ActivityTypeRequiredMessage = "Type cannot be empty.";
    public const string ActivityEndBeforeStartTimeMessage = "End time must be after start time.";
    public const string ModuleIdRequiredMessage = "Module ID cannot be empty.";
    public const string ActivityNameTooLongMessage = "Activity name too many characters.";
    public const string ActivityDescriptionTooLongMessage = "Description too many characters.";
    public const string EndDateMustBeAfterStartDateMessage = "End date must be after start date.";

    public const string ActivityNotFoundCode = "ACTIVITY_NOT_FOUND";
    public const string ActivityNotFoundMessage = "Activity not found.";
    public const string ActivityOverlapCode = "ACTIVITY_TIMESLOT_OVERLAP";
    public const string ActivityOverlapMessage = "Activity overlaps with an existing activity in this module.";
    public const string ActivityOutsideModuleCode = "ACTIVITY_OUTSIDE_MODULE";
    public const string ActivityOutsideModuleMessage = "Activity must be within the module timeframe.";

}
