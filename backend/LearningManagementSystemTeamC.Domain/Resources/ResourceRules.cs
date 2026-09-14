namespace LearningManagementSystemTeamC.Domain.Resources
{
    public class ResourceRules
    {
        public const int ResourceNameMaxLength = 200;
        public const int ContentMaxLength = 1000;
        public const int UrlMaxLength = 200;
        public const string ResourceNameRequiredMessage = "Resource name cannot be empty.";
        public const string ContentRequiredMessage = "Content cannot be empty.";
        public const string UserIdRequiredMessage = "User ID cannot be empty.";
        public const string ActivityIdRequiredMessage = "Activity ID cannot be empty.";
        public const string NotFoundCode = "RESOURCE_NOT_FOUND";
        public const string NotFoundMessage = "Resource not found.";
        public const string TypeIsRequiredMessage = "Type is required.";
        public const string ActivityCreationFailed = "ACTIVITY_CREATION_FAILED";
        public const string ActivityCreationFailedMessage = "Activity creation failed.";
        public const string CreatedAtCannotBeInTheFutureMessage = "CreatedAt cannot be in the future.";
        public const string CreatedAtIsRequiredMessage = "CreatedAt is required.";
    }
}
