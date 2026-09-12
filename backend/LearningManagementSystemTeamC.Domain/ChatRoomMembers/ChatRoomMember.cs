namespace LearningManagementSystemTeamC.Domain.ChatRoomMembers;

public class ChatRoomMember
{
    public Guid Id { get; private set; }
    public Guid ChatRoomId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime JoinedAt { get; private set; }

    private ChatRoomMember() { }

    internal ChatRoomMember(
        Guid chatRoomId,
        Guid userId)
    {
        Id = Guid.NewGuid();
        ChatRoomId = chatRoomId;
        UserId = userId;
        JoinedAt = DateTime.UtcNow;
    }
}