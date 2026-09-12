using LearningManagementSystemTeamC.Domain.ChatRoomMembers;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Domain.ChatRooms;

public class ChatRoom
{
    private readonly List<ChatRoomMember> _members = new();

    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<ChatRoomMember> Members => _members.AsReadOnly();

    private ChatRoom() { }

    public ChatRoom(string? name)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddMember(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new DomainException(
                ChatRoomRules.EmptyUserIdCode, ChatRoomRules.EmptyUserIdMessage);

        if (_members.Any(x => x.UserId == userId))
            throw new DomainException(
                ChatRoomRules.IsMemberCode, ChatRoomRules.IsMemberMessage);

        _members.Add(
            new ChatRoomMember(
                Id,
                userId));
    }

    public void RemoveMember(Guid userId)
    {
        var member = _members.FirstOrDefault(x => x.UserId == userId);

        if (member is null)
            throw new DomainException(
                ChatRoomRules.NotMemberCode, ChatRoomRules.NotMemberMessage);

        _members.Remove(member);
    }
}