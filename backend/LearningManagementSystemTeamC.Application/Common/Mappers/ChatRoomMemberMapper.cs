using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.ReadModels;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public class ChatRoomMemberMapper
{
    public static ChatRoomMemberDto ToDto(
        ChatRoomMemberReadModel model)
    {
        return new ChatRoomMemberDto(
            model.UserId,
            model.FirstName,
            model.LastName,
            model.Email,
            model.JoinedAt,
            model.IsActive);
    }
}
