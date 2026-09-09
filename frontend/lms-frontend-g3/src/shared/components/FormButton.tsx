interface FormButtonProps {
  children: React.ReactNode;
  type?: "button" | "submit" | "reset";
  disabled?: boolean;
  className?: string;
}

export function FormButton({
  children,
  type = "button",
  disabled = false,
  className = "",
}: FormButtonProps) {
  return (
    <button
      type={type}
      disabled={disabled}
      className={`w-full cursor-pointer rounded-md px-4 py-3 font-medium hover:brightness-75 disabled:cursor-not-allowed disabled:opacity-50 ${className}`}
    >
      {children}
    </button>
  );
}
