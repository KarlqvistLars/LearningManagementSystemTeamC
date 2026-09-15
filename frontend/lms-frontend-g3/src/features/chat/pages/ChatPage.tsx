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
import {
  chatConnection,
  startChatConnection,
} from "../services/chatConnection";

export function ChatPage() {
  const { chatRoomId } = useParams();

  const [chatRooms, setChatRooms] = useState<ChatRoom[]>([]);
  const [messages, setMessages] = useState<Message[]>([]);
  const [isLoadingRooms, setIsLoadingRooms] = useState(true);
  const [isLoadingMessages, setIsLoadingMessages] = useState(false);

  const { user } = useAuth();

  useEffect(() => {
    const connectAndJoinRoom = async () => {
      try {
        await startChatConnection();

        if (!chatRoomId) return;

        await chatConnection.invoke("JoinRoom", chatRoomId);

        console.log("Joined chat room:", chatRoomId);
      } catch (error) {
        console.error("Failed to connect or join chat room:", error);
      }
    };

    connectAndJoinRoom();
  }, [chatRoomId]);

  useEffect(() => {
    const handleReceiveMessage = (message: Message) => {
      setMessages((prev) => [...prev, message]);
    };

    chatConnection.on("ReceiveMessage", handleReceiveMessage);

    return () => {
      chatConnection.off("ReceiveMessage", handleReceiveMessage);
    };
  }, []);

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

    await sendMessage(chatRoomId, content);
  };

  if (!user) return null;

  if (isLoadingRooms) {
    return (
      <div className="flex h-full min-h-0 items-center justify-center">
        Loading...
      </div>
    );
  }

  return (
    <div className="flex h-full min-h-0 w-full overflow-hidden">
      <aside className="flex h-full min-h-0 w-80 shrink-0 flex-col border-r border-border bg-menu">
        <div className="shrink-0 border-b border-border px-4 py-4">
          <DisplayText text="Chat Conversations" />
        </div>

        <div className="min-h-0 flex-1 overflow-y-auto">
          <ChatRoomList
            chatRooms={chatRooms}
            currentUserId={user.id}
            selectedChatRoomId={chatRoomId}
          />
        </div>
      </aside>

      <section className="flex h-full min-h-0 min-w-0 flex-1 flex-col overflow-hidden">
        {!chatRoomId ? (
          <div className="flex min-h-0 flex-1 items-center justify-center">
            <DisplayText
              text="Select a conversation to start chatting."
              size="large"
            />
          </div>
        ) : isLoadingMessages ? (
          <div className="flex min-h-0 flex-1 items-center justify-center">
            <DisplayText text="Loading messages..." size="large" />
          </div>
        ) : (
          <>
            <MessageList messages={messages} currentUserId={user.id} />

            <MessageInput onSend={handleSendMessage} />
          </>
        )}
      </section>
    </div>
  );
}
