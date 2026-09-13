import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { ChatRoomList } from "../components/ChatRoomList";
import { getMyChatRooms } from "../api/chatRoomApi";
import type { ChatRoom } from "../types/chatRoom";
import { useAuth } from "../../auth/AuthContext";

export function ChatPage() {
  const { chatRoomId } = useParams();

  const [chatRooms, setChatRooms] = useState<ChatRoom[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  const { user } = useAuth();

  useEffect(() => {
    const loadChatRooms = async () => {
      try {
        const rooms = await getMyChatRooms();
        setChatRooms(rooms);
      } catch (error) {
        console.error(error);
      } finally {
        setIsLoading(false);
      }
    };

    loadChatRooms();
  }, []);

  if (!user) return null;

  if (isLoading) {
    return (
      <div className="flex h-full items-center justify-center">Loading...</div>
    );
  }

  return (
    <div className="flex h-full min-h-0">
      <aside className="w-80 shrink-0 border-r border-border bg-menu">
        <div className="border-b border-border px-4 py-4">
          <h1 className="text-xl font-semibold">Chat</h1>
        </div>

        <ChatRoomList
          chatRooms={chatRooms}
          currentUserId={user.id}
          selectedChatRoomId={chatRoomId}
        />
      </aside>

      <main className="flex min-w-0 flex-1 flex-col">
        {chatRoomId ? (
          <div className="flex flex-1 items-center justify-center">
            <p className="text-gray-400">Conversation will appear here.</p>
          </div>
        ) : (
          <div className="flex flex-1 items-center justify-center">
            <p className="text-gray-400">
              Select a conversation to start chatting.
            </p>
          </div>
        )}
      </main>
    </div>
  );
}
