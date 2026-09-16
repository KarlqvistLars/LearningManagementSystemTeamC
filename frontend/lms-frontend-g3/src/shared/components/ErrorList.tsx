import type { ApiError } from "../../api/types";

type ErrorListVariant = "list" | "form";

interface ErrorListProps {
  error?: ApiError;
  variant?: ErrorListVariant;
}

export function ErrorList({ error, variant = "list" }: ErrorListProps) {
  if (!error) {
    return null;
  }

  const messages = Object.values(error.details ?? {}).flat();
  const hasDetails = messages.length > 0;

  return (
    <div className="rounded-md border border-red-500 bg-red-500/10 p-4">
      {variant === "form" && hasDetails && (
        <p className="font-medium text-red-400">
          Please fix the following errors:
        </p>
      )}

      {hasDetails ? (
        <ul className="mt-2 list-disc pl-5 text-red-400">
          {messages.map((message, index) => (
            <li key={index}>{message}</li>
          ))}
        </ul>
      ) : (
        <p className="text-red-400">{error.message}</p>
      )}
    </div>
  );
}
