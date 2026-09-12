using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.ChatRooms;

namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.CreateChatRoom;

public class CreateChatRoomValidator : IValidator<CreateChatRoomCommand>
{
    public Dictionary<string, string[]> Validate(
        CreateChatRoomCommand command)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            errors[nameof(command.Name)] =
            [
                ChatRoomRules.NameRequiredMessage
            ];
        }
        else if (command.Name.Length > ChatRoomRules.ChatRoomNameMaxLength)
        {
            errors[nameof(command.Name)] =
            [
                ChatRoomRules.NameTooLongMessage(ChatRoomRules.ChatRoomNameMaxLength)
            ];
        }

        if (command.MemberIds.Count == 0)
        {
            errors[nameof(command.MemberIds)] =
            [
                ChatRoomRules.MemberRequiredMessage
            ];
        }

        return errors;
    }
}