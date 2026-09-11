namespace LearningManagementSystemTeamC.Application.Auth.Commands.ForgotPassword;

public static class ForgotPasswordRules
{
    public const int TokenHashMaxSize = 64;
    public const string EmailEmptyMessage = "Email is empty";
    public const string ResetEmailSentMessage = "If an account exists with this email, a reset link has been sent.";

}
