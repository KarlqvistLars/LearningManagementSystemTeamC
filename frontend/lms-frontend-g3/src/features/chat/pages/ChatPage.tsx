import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { ChatRoomList } from "../components/ChatRoomList";
import { getMyChatRooms } from "../api/chatRoomApi";
import { getMessages, sendMessage } from "../../messages/api/messageApi";
import { MessageList } from "../../messages/components/MessageList";
import { MessageInput } from "../../messages/components/MessageInput";
import type { ChatRoom } from "../types/chatRoom";
import type { Message } from "../../messages/types/message";
import { useAuth } from "../../auth/AuthContext";
import { DisplayText } from "../../../shared/components/DisplayText";

export function ChatPage() {
  const { chatRoomId } = useParams();

  const [chatRooms, setChatRooms] = useState<ChatRoom[]>([]);
  const [messages, setMessages] = useState<Message[]>([]);
  const [isLoadingRooms, setIsLoadingRooms] = useState(true);
  const [isLoadingMessages, setIsLoadingMessages] = useState(false);

  const { user } = useAuth();

  useEffect(() => {
    const loadChatRooms = async () => {
      try {
        const rooms = await getMyChatRooms();
        setChatRooms(rooms);
      } catch (error) {
        console.error(error);
      } finally {
        setIsLoadingRooms(false);
      }
    };

    loadChatRooms();
  }, []);

  useEffect(() => {
    if (!chatRoomId) return;

    const loadMessages = async () => {
      setIsLoadingMessages(true);

      try {
        const result = await getMessages(chatRoomId);
        setMessages(result);
      } catch (error) {
        console.error(error);
        setMessages([]);
      } finally {
        setIsLoadingMessages(false);
      }
    };

    loadMessages();
  }, [chatRoomId]);

  const handleSendMessage = async (content: string) => {
    if (!chatRoomId) return;

    const message = await sendMessage(chatRoomId, content);

    setMessages((prev) => [...prev, message]);
  };

  if (!user) return null;

  if (isLoadingRooms) {
    return (
      <div className="flex h-full items-center justify-center">Loading...</div>
    );
  }

  return (
    <div className="flex h-full min-h-0">
      <aside className="w-80 shrink-0 border-r border-border bg-menu">
        <div className="border-b border-border px-4 py-4">
          <DisplayText text="Chat Conversations" />
        </div>

        <ChatRoomList
          chatRooms={chatRooms}
          currentUserId={user.id}
          selectedChatRoomId={chatRoomId}
        />
      </aside>

      <main className="flex min-w-0 flex-1 flex-col">
        {!chatRoomId ? (
          <div className="flex flex-1 items-center justify-center">
            <DisplayText
              text="Select a conversation to start chatting."
              size="large"
            />
          </div>
        ) : isLoadingMessages ? (
          <div className="flex flex-1 items-center justify-center">
            <DisplayText text="Loading messages..." size="large" />
          </div>
        ) : (
          <>
            <MessageList messages={messages} currentUserId={user.id} />

            <MessageInput onSend={handleSendMessage} />
          </>
        )}
      </main>
    </div>
  );
}
