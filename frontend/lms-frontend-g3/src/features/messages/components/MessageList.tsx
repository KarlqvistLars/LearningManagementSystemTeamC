import type { Message } from "../types/message";
import { MessageListItem } from "./MessageListItem";

interface MessageListProps {
  messages: Message[];
  currentUserId: string;
}

export function MessageList({ messages, currentUserId }: MessageListProps) {
  if (messages.length === 0) {
    return (
      <div className="flex flex-1 items-center justify-center">
        <p className="text-gray-400">No messages yet.</p>
      </div>
    );
  }

  return (
    <div className="flex flex-1 flex-col gap-3 overflow-y-auto p-4">
      {messages.map((message) => (
        <MessageListItem
          key={message.id}
          message={message}
          isOwnMessage={message.senderId === currentUserId}
        />
      ))}
    </div>
  );
}
