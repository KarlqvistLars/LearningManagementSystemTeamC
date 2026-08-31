namespace LearningManagementSystemTeamC.Domain.Courses;

public static class CourseRules
{
    public const int CourseNameMaxLength = 200;
    public const int DescriptionMaxLength = 2000;

    public const string CourseNameRequiredCode = "COURSE_NAME_REQUIRED";
    public const string CourseNameRequiredMessage = "Course name is required.";

    public const string CourseNameTooLongCode = "COURSE_NAME_TOO_LONG";
    public const string CourseNameTooLongMessage =
        "Course name cannot exceed 200 characters.";

    public const string DescriptionRequiredCode = "COURSE_DESCRIPTION_REQUIRED";
    public const string DescriptionRequiredMessage =
        "Description is required.";

    public const string DescriptionTooLongCode = "COURSE_DESCRIPTION_TOO_LONG";
    public const string DescriptionTooLongMessage =
        "Description cannot exceed 2000 characters.";

    public const string InvalidDatesCode = "COURSE_INVALID_DATES";
    public const string InvalidDatesMessage =
        "End date must be after start date.";
}