namespace LearningManagementSystemTeamC.Api.Common.Constants;

public static class ExceptionConstants
{
    public const string DefaultExceptionCode = "SERVER_ERROR";
    public const string DefaultExceptionMessage = "Something went wrong";

    public const string UnauthorizedCode = "UNAUTHORIZED";
    public const string UnauthorizedMessage = "Authentication is required.";

    public const string ForbiddenCode = "FORBIDDEN";
    public const string ForbiddenMessage =
        "You do not have permission to access this resource.";

    public const string NotFoundCode = "NOT_FOUND";
    public const string NotFoundMessage =
        "The requested resource was not found.";

    public const string MethodNotAllowedCode = "METHOD_NOT_ALLOWED";
    public const string MethodNotAllowedMessage =
        "The HTTP method is not allowed.";

    public const string HttpErrorCode = "HTTP_ERROR";
    public const string HttpErrorMessage = "An HTTP error occurred.";
}