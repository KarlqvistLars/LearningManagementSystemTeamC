namespace LearningManagementSystemTeamC.Domain.Messages;

public class Message
{
    public Guid Id { get; private set; }
    public Guid ChatRoomId { get; private set; }
    public Guid SenderId { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private Message() { }

    public Message(
        Guid chatRoomId,
        Guid senderId,
        string content)
    {
        Id = Guid.NewGuid();
        ChatRoomId = chatRoomId;
        SenderId = senderId;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }
}