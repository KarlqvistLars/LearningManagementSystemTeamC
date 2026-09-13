import { useEffect, useRef } from "react";
import type { Message } from "../types/message";
import { MessageListItem } from "./MessageListItem";
import { DisplayText } from "../../../shared/components/DisplayText";

interface MessageListProps {
  messages: Message[];
  currentUserId: string;
}

export function MessageList({ messages, currentUserId }: MessageListProps) {
  const messageListRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const element = messageListRef.current;

    if (!element) return;

    element.scrollTop = element.scrollHeight;
  }, [messages]);

  if (messages.length === 0) {
    return (
      <div className="flex min-h-0 flex-1 items-center justify-center">
        <DisplayText text="No messages yet." size="large" />
      </div>
    );
  }

  return (
    <div ref={messageListRef} className="min-h-0 flex-1 overflow-y-auto p-4">
      <div className="flex flex-col gap-3">
        {messages.map((message) => (
          <MessageListItem
            key={message.id}
            message={message}
            isOwnMessage={message.senderId === currentUserId}
          />
        ))}
      </div>
    </div>
  );
}
