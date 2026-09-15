type ErrorListVariant = "list" | "form";

interface ErrorListProps {
  errors: Record<string, string[]>;
  variant?: ErrorListVariant;
}

export function ErrorList({ errors, variant = "list" }: ErrorListProps) {
  const messages = Object.values(errors).flat();

  if (messages.length === 0) {
    return null;
  }

  return (
    <div className="rounded-md border border-red-500 bg-red-500/10 p-4">
      {variant === "form" && (
        <p className="font-medium text-red-400">
          Please fix the following errors:
        </p>
      )}

      <ul
        className={
          variant === "form"
            ? "mt-2 list-disc pl-5 text-red-400"
            : "list-disc pl-5 text-red-400"
        }
      >
        {messages.map((message, index) => (
          <li key={index}>{message}</li>
        ))}
      </ul>
    </div>
  );
}
