import { useNavigate } from "react-router";
import type { ChatRoom } from "../types/chatRoom";
import ChatRoomConstants from "../constants/ChatRoomConstants";

interface ChatRoomListProps {
  chatRooms: ChatRoom[];
  currentUserId: string;
  selectedChatRoomId?: string;
}

export function ChatRoomList({
  chatRooms,
  currentUserId,
  selectedChatRoomId,
}: ChatRoomListProps) {
  const navigate = useNavigate();

  const getChatRoomName = (chatRoom: ChatRoom) => {
    if (chatRoom.name) return chatRoom.name;

    const otherMember = chatRoom.members.find(
      (member) => member.userId !== currentUserId,
    );

    return otherMember
      ? `${otherMember.firstName} ${otherMember.lastName}`
      : ChatRoomConstants.DefaultChatRoomName;
  };

  if (chatRooms.length === 0) {
    return (
      <div className="p-4">
        <p className="text-sm text-gray-400">No conversations yet.</p>
      </div>
    );
  }

  return (
    <div className="flex flex-col gap-1 p-2">
      {chatRooms.map((chatRoom) => {
        const isSelected = chatRoom.id === selectedChatRoomId;

        return (
          <button
            key={chatRoom.id}
            type="button"
            onClick={() => navigate(`/chat/${chatRoom.id}`)}
            className={`cursor-pointer rounded-lg px-4 py-3 text-left transition ${
              isSelected
                ? "bg-button-create text-button-create-text"
                : "hover:bg-background"
            }`}
          >
            <p className="font-medium">{getChatRoomName(chatRoom)}</p>
          </button>
        );
      })}
    </div>
  );
}
