namespace LearningManagementSystemTeamC.Application.Auth.Commands.ResetPassword;

public class ResetPasswordRules
{
    public const string TokenInvalidCode = "RESET_PASSWORD_TOKEN_INVALID";
    public const string TokenInvalidMessage = "The password reset link is invalid or has expired";
    public const string ResetTokenRequiredMessage = "Reset token is required";
    public const string NewPasswordRequiredMessage = "New password is required";
    public const string ResetPasswordSuccessMessage = "Your password has been reset successfully";
}
