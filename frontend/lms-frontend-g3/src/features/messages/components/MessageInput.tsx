import { useState } from "react";
import { Button } from "../../../shared/components/Button";

interface MessageInputProps {
  onSend: (content: string) => Promise<void>;
  disabled?: boolean;
}

export function MessageInput({ onSend, disabled = false }: MessageInputProps) {
  const [content, setContent] = useState("");
  const [isSending, setIsSending] = useState(false);

  const handleSend = async () => {
    const trimmedContent = content.trim();

    if (!trimmedContent || isSending || disabled) return;

    setIsSending(true);

    try {
      await onSend(trimmedContent);
      setContent("");
    } finally {
      setIsSending(false);
    }
  };

  const handleKeyDown = (event: React.KeyboardEvent<HTMLTextAreaElement>) => {
    if (event.key !== "Enter" || event.shiftKey) return;

    event.preventDefault();
    handleSend();
  };

  return (
    <div className="flex shrink-0 items-end gap-3 border-t border-border bg-menu p-4">
      <textarea
        value={content}
        onChange={(event) => setContent(event.target.value)}
        onKeyDown={handleKeyDown}
        disabled={disabled || isSending}
        placeholder="Write a message..."
        rows={1}
        className="min-h-12 flex-1 resize-none rounded-lg border border-border bg-background px-4 py-3 text-primary-title-text outline-none focus:border-button-create disabled:cursor-not-allowed disabled:opacity-50"
      />

      <div className="flex self-center">
        <Button
          type="button"
          variant="list"
          color="create"
          disabled={disabled || isSending || content.trim().length === 0}
          onClick={handleSend}
        >
          {isSending ? "Sending..." : "Send"}
        </Button>
      </div>
    </div>
  );
}
