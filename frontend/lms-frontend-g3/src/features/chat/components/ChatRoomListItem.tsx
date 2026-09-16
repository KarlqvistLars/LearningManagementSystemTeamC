import type { ChatRoom } from "../types/chatRoom";
import ChatRoomConstants from "../constants/ChatRoomConstants";

interface ChatRoomListItemProps {
  chatRoom: ChatRoom;
  currentUserId: string;
  isSelected: boolean;
  onClick: () => void;
}

export function ChatRoomListItem({
  chatRoom,
  currentUserId,
  isSelected,
  onClick,
}: ChatRoomListItemProps) {
  const getChatRoomName = () => {
    if (chatRoom.name) return chatRoom.name;

    const otherMember = chatRoom.members.find(
      (member) => member.userId !== currentUserId,
    );

    if (!otherMember) return ChatRoomConstants.UnknownUser;

    return `${otherMember.firstName} ${otherMember.lastName}`;
  };

  return (
    <button
      type="button"
      onClick={onClick}
      className={`w-full cursor-pointer rounded-lg px-3 py-3 text-left transition ${
        isSelected
          ? "bg-side-menu-bg text-side-menu-text"
          : " text-primary-display-text hover:bg-side-menu-bg/50"
      }`}
    >
      <p className="font-medium">{getChatRoomName()}</p>
    </button>
  );
}
