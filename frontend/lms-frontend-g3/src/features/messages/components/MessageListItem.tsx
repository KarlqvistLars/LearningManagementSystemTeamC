import { DisplayText } from "../../../shared/components/DisplayText";
import { formatMessageTime } from "../../../shared/utils/formateMessageTime";
import type { Message } from "../types/message";

interface MessageListItemProps {
  message: Message;
  isOwnMessage: boolean;
}

export function MessageListItem({
  message,
  isOwnMessage,
}: MessageListItemProps) {
  return (
    <div className={`flex ${isOwnMessage ? "justify-end" : "justify-start"}`}>
      <div
        className={`max-w-[70%] rounded-xl px-4 py-2 ${
          isOwnMessage
            ? "bg-chat-own-bg text-chat-own-text"
            : "bg-chat-others-bg text-chat-others-text"
        }`}
      >
        {!isOwnMessage && (
          <DisplayText
            text={`${message.senderFirstName} ${message.senderLastName}`}
            size="small"
          />
        )}

        <p className="wrap-break-words">{message.content}</p>

        <p className="mt-1 text-xs opacity-60">
          {formatMessageTime(message.createdAt)}
        </p>
      </div>
    </div>
  );
}
