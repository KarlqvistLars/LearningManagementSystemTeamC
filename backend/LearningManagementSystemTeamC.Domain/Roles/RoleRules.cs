namespace LearningManagementSystemTeamC.Domain.Roles;

public class RoleRules
{
    public const int NameMaxLength = 50;
    public const int CodeMaxLength = 50;

    public const string NameEmptyCode = "ROLE_NAME_EMPTY";
    public const string NameEmptyMessage = "Role name can not be empty";

    public const string NameTooLongCode = "ROLE_NAME_TOO_LONG";

    public static string NameTooLongMessage(int length) =>
        $"Role name can not be longer than {length} characters.";

    public const string CodeEmptyCode = "ROLE_CODE_EMPTY";
    public const string CodeEmptyMessage = "Role code can not be empty";

    public const string CodeTooLongCode = "ROLE_CODE_TOO_LONG";

    public static string CodeTooLongMessage(int length) =>
        $"Role code can not be longer than {length} characters.";

    public const string DefaultRoleNotFoundCode = "ROLE_DEFAULT_NOT_FOUND";
    public const string DefaultRoleNotFoundMessage = "Default role group is not found";

    public const string RoleNotFoundCode = "ROLE_NOT_FOUND";
    public const string RoleNotFoundMessage = "Role is not found";
}
