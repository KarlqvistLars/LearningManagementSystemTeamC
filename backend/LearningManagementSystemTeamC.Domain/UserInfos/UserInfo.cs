using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Domain.UserInfos;

public class UserInfo
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; private set; }
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string PostalCode { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;

    private UserInfo() { }

    public UserInfo(Guid userId, string fName, string lName, DateTime dateOfBirth, string phoneNumber, string address, string postalCode, string city)
    {
        Validate(
            userId,
            fName,
            lName,
            dateOfBirth,
            phoneNumber,
            address,
            postalCode,
            city);

        Id = Guid.NewGuid();
        UserId = userId;
        FirstName = fName;
        LastName = lName;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Address = address;
        PostalCode = postalCode;
        City = city;
    }

    private static void Validate(
        Guid userId,
        string fName,
        string lName,
        DateTime dateOfBirth,
        string phoneNumber,
        string address,
        string postalCode,
        string city)
    {
        if (userId == Guid.Empty)
            throw new DomainException(UserInfoRules.UserIdRequiredCode, UserInfoRules.UserIdRequiredMessage);

        if (string.IsNullOrWhiteSpace(fName))
            throw new DomainException(UserInfoRules.FirstNameRequiredCode, UserInfoRules.FirstNameRequiredMessage);

        if (string.IsNullOrWhiteSpace(lName))
            throw new DomainException(UserInfoRules.LastNameRequiredCode, UserInfoRules.LastNameRequiredMessage);

        if (dateOfBirth == DateTime.MinValue)
            throw new DomainException(UserInfoRules.DateOfBirthRequiredCode, UserInfoRules.DateOfBirthRequiredMessage);

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new DomainException(UserInfoRules.PhoneNumberRequiredCode, UserInfoRules.PhoneNumberRequiredMessage);

        if (string.IsNullOrWhiteSpace(address))
            throw new DomainException(UserInfoRules.AddressRequiredCode, UserInfoRules.AddressRequiredMessage);

        if (string.IsNullOrWhiteSpace(postalCode))
            throw new DomainException(UserInfoRules.PostalCodeRequiredCode, UserInfoRules.PostalCodeRequiredMessage);

        if (string.IsNullOrWhiteSpace(city))
            throw new DomainException(UserInfoRules.CityRequiredCode, UserInfoRules.CityRequiredMessage);
    }
}
