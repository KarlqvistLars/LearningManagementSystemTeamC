namespace LearningManagementSystemTeamC.Application.ChatRooms.Commands.CreateChatRoom;

public record CreateChatRoomCommand(
    string? Name,
    List<Guid> MemberIds);