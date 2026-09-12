namespace LearningManagementSystemTeamC.Domain.ChatRooms;

public class ChatRoomRules
{
    public const int ChatRoomNameMaxLength = 100;

    public const string EmptyUserIdCode = "CHAT_ROOM_EMPTY_USER_ID";
    public const string EmptyUserIdMessage = "User id can not be empty";

    public const string IsMemberCode = "CHAT_ROOM_USER_IS_MEMBER";
    public const string IsMemberMessage = "User is already a member of this chat room.";
    public const string NotMemberCode = "CHAT_ROOM_USER_NOT_MEMBER";
    public const string NotMemberMessage = "User is not a member of this chat room";

    public const string CreateReadFailedCode = "CHAT_ROOM_CREATE_READ_FAIL";
    public const string CreateReadFailedMessage =
        "Chat room was created but could not be retrieved.";

    public const string NameRequiredMessage = "Chat room name is required.";
    public static string NameTooLongMessage(int maxLength) => $"Chat room name cannot exceed {maxLength} characters.";
    public const string MemberRequiredMessage = "At least one member is required.";

    public const string NotFoundCode = "CHAT_ROOM_NOT_FOUND";
    public const string NotFoundMessage = "Chat room could not be found";

    public const string NotOwnerCode = "CHAT_ROOM_NOT_OWNER";
    public const string NotOwnerMessage = "You are not the owner of the chatroom";
}
