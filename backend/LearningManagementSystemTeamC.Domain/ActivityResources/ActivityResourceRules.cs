namespace LearningManagementSystemTeamC.Domain.ActivityResources
{
    public class ActivityResourceRules
    {
        public const int ResourceNameMaxLength = 200;
        public const int ContentMaxLength = 1000;
        public const int UrlMaxLength = 200;
        public const string ResourceNameRequiredMessage = "Resource name cannot be empty.";
        public const string ContentRequiredMessage = "Content cannot be empty.";
        public const string UserIdRequiredMessage = "User ID cannot be empty.";
        public const string ActivityIdRequiredMessage = "Activity ID cannot be empty.";

        public const string ActivityResourceCode = "ACTIVITY_NOT_FOUND";
        public const string ActivityResourcesNotFound = "Activity resources not found.";


    }
}
