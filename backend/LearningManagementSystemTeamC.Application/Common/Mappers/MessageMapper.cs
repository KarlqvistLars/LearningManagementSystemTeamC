using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.Messages;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class MessageMapper
{
    public static MessageDto ToDto(Message message)
    {
        return new MessageDto(
            message.Id,
            message.ChatRoomId,
            message.SenderId,
            message.Content,
            message.CreatedAt);
    }
}