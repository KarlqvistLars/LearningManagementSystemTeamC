using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Messages.Commands.SendMessage;

public interface ISendMessageHandler
{
    Task<MessageDto> HandleAsync(
        SendMessageCommand command,
        Guid currentUserId,
        CancellationToken cancellationToken);
}