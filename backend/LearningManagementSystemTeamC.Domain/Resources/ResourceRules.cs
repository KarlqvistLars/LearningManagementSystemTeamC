namespace LearningManagementSystemTeamC.Domain.Resources
{
    public class ResourceRules
    {
        public const int ResourceNameMaxLength = 200;
        public const int ContentMaxLength = 1000;
        public const int UrlMaxLength = 200;

        public const string ResourceNameRequiredCode = "RESOURCE_NAME_REQUIRED";
        public const string ResourceNameRequiredMessage = "Resource name cannot be empty.";

        public const string ContentRequiredCode = "RESOURCE_CONTENT_REQUIRED";
        public const string ContentRequiredMessage = "Content cannot be empty.";

        public const string UserIdRequiredCode = "RESOURCE_USER_ID_REQUIRED";
        public const string UserIdRequiredMessage = "User ID cannot be empty.";

        public const string ActivityIdRequiredCode = "RESOURCE_ACTIVITY_ID_REQUIRED";
        public const string ActivityIdRequiredMessage = "Activity ID cannot be empty.";

        public const string CreatedByRequiredCode = "RESOURCE_CREATED_BY_REQUIRED";
        public const string CreatedByRequiredMessage = "Resource creator is required.";

        public const string CreatedAtRequiredCode = "RESOURCE_CREATED_AT_REQUIRED";
        public const string CreatedAtRequiredMessage = "CreatedAt is required.";

        public const string CreatedAtCannotBeInTheFutureCode = "RESOURCE_CREATED_AT_IN_FUTURE";
        public const string CreatedAtCannotBeInTheFutureMessage = "CreatedAt cannot be in the future.";

        public const string InvalidResourceTypeCode = "INVALID_RESOURCE_TYPE";
        public const string InvalidResourceTypeMessage = "Resource type is invalid.";

        public const string InvalidUrlCode = "INVALID_RESOURCE_URL";
        public const string InvalidUrlMessage = "Resource URL is invalid.";

        public const string NotFoundCode = "RESOURCE_NOT_FOUND";
        public const string NotFoundMessage = "Resource not found.";

        public const string TypeIsRequiredCode = "RESOURCE_TYPE_REQUIRED";
        public const string TypeIsRequiredMessage = "Type is required.";

        public const string ActivityCreationFailed = "ACTIVITY_CREATION_FAILED";
        public const string ActivityCreationFailedMessage = "Activity creation failed.";

        public const string InvalidStudentResourceTypeCode = "INVALID_STUDENT_RESOURCE_TYPE";
        public const string InvalidStudentResourceTypeMessage = "Students can only create submission resources.";

        public const string ResourceUpdateNotAllowedCode = "RESOURCE_UPDATE_NOT_ALLOWED";
        public const string ResourceUpdateNotAllowedMessage = "You are not allowed to update this resource.";

    }
}