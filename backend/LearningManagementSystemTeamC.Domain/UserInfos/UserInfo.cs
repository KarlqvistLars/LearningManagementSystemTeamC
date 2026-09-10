using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Domain.UserInfos;

public class UserInfo
{
    public Guid UserId { get; private set; }

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;

    public DateTime? DateOfBirth { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? Address { get; private set; }
    public string? PostalCode { get; private set; }
    public string? City { get; private set; }

    private UserInfo() { }

    public UserInfo(
        Guid userId,
        string firstName,
        string lastName)
    {
        Validate(
            userId,
            firstName,
            lastName);

        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
    }

    public void UpdateProfile(
        string firstName,
        string lastName,
        DateTime? dateOfBirth,
        string? phoneNumber,
        string? address,
        string? postalCode,
        string? city)
    {
        ValidateName(
            firstName,
            lastName);

        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Address = address;
        PostalCode = postalCode;
        City = city;
    }

    private static void Validate(
        Guid userId,
        string firstName,
        string lastName)
    {
        if (userId == Guid.Empty)
            throw new DomainException(
                UserInfoRules.UserIdRequiredCode,
                UserInfoRules.UserIdRequiredMessage);

        ValidateName(
            firstName,
            lastName);
    }

    private static void ValidateName(
        string firstName,
        string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException(
                UserInfoRules.FirstNameRequiredCode,
                UserInfoRules.FirstNameRequiredMessage);

        if (firstName.Length > UserInfoRules.FirstNameMaxLength)
            throw new DomainException(
                UserInfoRules.FirstNameTooLongCode,
                UserInfoRules.FirstNameTooLongMessage(
                    UserInfoRules.FirstNameMaxLength));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException(
                UserInfoRules.LastNameRequiredCode,
                UserInfoRules.LastNameRequiredMessage);

        if (lastName.Length > UserInfoRules.LastNameMaxLength)
            throw new DomainException(
                UserInfoRules.LastNameTooLongCode,
                UserInfoRules.LastNameTooLongMessage(
                    UserInfoRules.LastNameMaxLength));
    }
}