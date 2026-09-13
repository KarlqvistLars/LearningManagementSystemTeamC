import { useNavigate } from "react-router";
import type { ChatRoom } from "../types/chatRoom";
import { ChatRoomListItem } from "./ChatRoomListItem";

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

  if (chatRooms.length === 0) {
    return (
      <div className="p-4">
        <p className="text-sm text-gray-400">No conversations yet.</p>
      </div>
    );
  }

  return (
    <div className="flex flex-col gap-1 p-2">
      {chatRooms.map((chatRoom) => (
        <ChatRoomListItem
          key={chatRoom.id}
          chatRoom={chatRoom}
          currentUserId={currentUserId}
          isSelected={chatRoom.id === selectedChatRoomId}
          onClick={() => navigate(`/chat/${chatRoom.id}`)}
        />
      ))}
    </div>
  );
}
