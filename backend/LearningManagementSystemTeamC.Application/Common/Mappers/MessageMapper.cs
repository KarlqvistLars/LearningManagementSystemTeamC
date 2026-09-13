using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.ReadModels;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class MessageMapper
{
    public static MessageDto ToDto(MessageReadModel model)
    {
        return new MessageDto(
            model.Id,
            model.SenderId,
            model.FirstName,
            model.LastName,
            model.Content,
            model.CreatedAt);
    }
}