namespace LearningManagementSystemTeamC.Domain.Messages;

public class MessageRules
{
    public const int MessageContentMaxLength = 4000;

    public const string ContentRequiredCode = "MESSAGE_CONTENT_REQUIRED";
    public const string ContentRequiredMessage =
        "Message content is required.";

    public const string ContentTooLongCode = "MESSAGE_CONTENT_TOO_LONG";
    public static string ContentTooLongMessage(int maxLength) =>
        $"Message content cannot exceed {maxLength} characters.";

    public const string CreateReadFailedCode = "MESSAGE_CREATE_READ_FAIL";
    public const string CreateReadFailedMessage = "Message is created but failed to retrive";
}
