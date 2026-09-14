using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Services;
using LearningManagementSystemTeamC.Domain.UserInfos;

namespace LearningManagementSystemTeamC.Application.Users.Commands.UpdateUser;

public class UpdateUserValidator : IValidator<UpdateUserCommand>
{
    public Dictionary<string, string[]> Validate(
        UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var errors = new Dictionary<string, string[]>();

        UserValidation.ValidateEmail(
            command.Email,
            errors,
            nameof(command.Email));

        UserValidation.ValidateFirstName(
            command.FirstName,
            errors,
            nameof(command.FirstName));

        UserValidation.ValidateLastName(
            command.LastName,
            errors,
            nameof(command.LastName));

        if (command.PhoneNumber is not null &&
            command.PhoneNumber.Length > UserInfoRules.PhoneNumberMaxLength)
        {
            errors[nameof(command.PhoneNumber)] =
            [
                UserInfoRules.PhoneNumberTooLongMessage(
                    UserInfoRules.PhoneNumberMaxLength)
            ];
        }

        if (command.Address is not null &&
            command.Address.Length > UserInfoRules.AddressMaxLength)
        {
            errors[nameof(command.Address)] =
            [
                UserInfoRules.AddressTooLongMessage(
                    UserInfoRules.AddressMaxLength)
            ];
        }

        if (command.PostalCode is not null &&
            command.PostalCode.Length > UserInfoRules.PostalCodeMaxLength)
        {
            errors[nameof(command.PostalCode)] =
            [
                UserInfoRules.PostalCodeTooLongMessage(
                    UserInfoRules.PostalCodeMaxLength)
            ];
        }

        if (command.City is not null &&
            command.City.Length > UserInfoRules.CityMaxLength)
        {
            errors[nameof(command.City)] =
            [
                UserInfoRules.CityTooLongMessage(
                    UserInfoRules.CityMaxLength)
            ];
        }

        return errors;
    }
}