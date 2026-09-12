using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.ReadModels;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class ChatRoomMapper
{
    public static ChatRoomDto ToDto(
        ChatRoomReadModel model)
    {
        return new ChatRoomDto(
            model.Id,
            model.Name,
            model.CreatedAt,
            model.Members
                .Select(ChatRoomMemberMapper.ToDto)
                .ToList());
    }
}