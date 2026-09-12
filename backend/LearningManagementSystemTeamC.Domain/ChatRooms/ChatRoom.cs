namespace LearningManagementSystemTeamC.Domain.ChatRooms;

public class ChatRoom
{
    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private ChatRoom() { }

    public ChatRoom(string? name)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }
}