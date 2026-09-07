namespace LearningManagementSystemTeamC.Domain.Enrollments;

public class EnrollmentRules
{
    // Behövs det rules för enrollment då det mest är en koppling mellan elev och kurs?
    public const int UserIdMaxLength = 36;
    public const string UserIdRequiredCode = "USER_ID_REQUIRED";
    public const string UserIdRequiredMessage = "User ID is required.";
}
