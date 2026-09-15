import { useEffect, useState } from "react";

interface ResponseMessageProps {
  message: string;
  type?: "message" | "error" | "success";
  duration?: number;
  onClose: () => void;
}

export function ResponseMessage({
  message,
  type = "message",
  duration = 3000,
  onClose,
}: ResponseMessageProps) {
  const [visible, setVisible] = useState(false);

  useEffect(() => {
    const showTimer = setTimeout(() => {
      setVisible(true);
    }, 10);

    const hideTimer = setTimeout(() => {
      setVisible(false);
    }, duration);

    const closeTimer = setTimeout(() => {
      onClose();
    }, duration + 500);

    return () => {
      clearTimeout(showTimer);
      clearTimeout(hideTimer);
      clearTimeout(closeTimer);
    };
  }, [duration, onClose]);

  const baseClasses =
    "fixed bottom-0 right-0 rounded-lg p-4 text-md transition-transform duration-500";

  const typeClasses =
    type === "error"
      ? "bg-red-100 text-red-700"
      : type === "success"
        ? "bg-green-100 text-green-700"
        : "bg-blue-100 text-blue-700";

  return (
    <div
      className={`${baseClasses} ${typeClasses} ${
        visible ? "translate-y-0" : "translate-y-full"
      }`}
      role="alert"
    >
      {message}
    </div>
  );
}
