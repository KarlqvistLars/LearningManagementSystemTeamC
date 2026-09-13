using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.Messages;

namespace LearningManagementSystemTeamC.Application.Messages.Commands.SendMessage;

public class SendMessageValidator : IValidator<SendMessageCommand>
{
    public Dictionary<string, string[]> Validate(
        SendMessageCommand command)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Content))
        {
            errors[nameof(command.Content)] =
            [
                MessageRules.ContentRequiredMessage
            ];
        }
        else if (command.Content.Length > MessageRules.MessageContentMaxLength)
        {
            errors[nameof(command.Content)] =
            [
                MessageRules.ContentTooLongMessage(
                    MessageRules.MessageContentMaxLength)
            ];
        }

        return errors;
    }
}