namespace LearningManagementSystemTeamC.Domain.UserInfos;

public static class UserInfoRules
{
    public const string UserIdRequiredCode = "USERINFO_USERID_REQUIRED";
    public const string UserIdRequiredMessage = "User ID is required.";

    public const string FirstNameRequiredCode = "USERINFO_FIRSTNAME_REQUIRED";
    public const string FirstNameRequiredMessage = "First name is required.";
    public const int FirstNameMaxLength = 100;
    public const string FirstNameTooLongCode = "USERINFO_FIRSTNAME_TOO_LONG";

    public static string FirstNameTooLongMessage(int maxLength)
        => $"First name cannot exceed {maxLength} characters.";

    public const string LastNameRequiredCode = "USERINFO_LASTNAME_REQUIRED";
    public const string LastNameRequiredMessage = "Last name is required.";
    public const int LastNameMaxLength = 100;
    public const string LastNameTooLongCode = "USERINFO_LASTNAME_TOO_LONG";

    public static string LastNameTooLongMessage(int maxLength)
        => $"Last name cannot exceed {maxLength} characters.";

    public const int PhoneNumberMaxLength = 30;
    public const string PhoneNumberTooLongCode = "USERINFO_PHONENUMBER_TOO_LONG";

    public static string PhoneNumberTooLongMessage(int maxLength)
        => $"Phone number cannot exceed {maxLength} characters.";

    public const int AddressMaxLength = 200;
    public const string AddressTooLongCode = "USERINFO_ADDRESS_TOO_LONG";

    public static string AddressTooLongMessage(int maxLength)
        => $"Address cannot exceed {maxLength} characters.";

    public const int PostalCodeMaxLength = 20;
    public const string PostalCodeTooLongCode = "USERINFO_POSTALCODE_TOO_LONG";

    public static string PostalCodeTooLongMessage(int maxLength)
        => $"Postal code cannot exceed {maxLength} characters.";

    public const int CityMaxLength = 100;
    public const string CityTooLongCode = "USERINFO_CITY_TOO_LONG";

    public static string CityTooLongMessage(int maxLength)
        => $"City cannot exceed {maxLength} characters.";
}
