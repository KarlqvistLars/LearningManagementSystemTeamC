export type ButtonVariant = "form" | "list";

export type ButtonColor = "create" | "edit" | "delete" | "cancel" | "resource";

interface ButtonProps {
  children: React.ReactNode;
  type?: "button" | "submit" | "reset";
  disabled?: boolean;
  variant?: ButtonVariant;
  color?: ButtonColor;
  onClick?: () => void;
}

const buttonVariants: Record<ButtonVariant, string> = {
  form: "w-full rounded-lg px-4 py-3 font-medium",
  list: "min-w-16 rounded-lg px-6 py-2 text-sm",
};

const buttonColors: Record<ButtonColor, string> = {
  create: "bg-button-create text-button-create-text",
  edit: "bg-button-edit text-button-edit-text",
  delete: "bg-button-delete text-button-delete-text",
  cancel: "bg-button-delete text-button-delete-text",
  resource: "bg-button-resource text-button-resource-text",
};

export function Button({
  children,
  type = "button",
  disabled = false,
  variant = "form",
  color = "create",
  onClick,
}: ButtonProps) {
  return (
    <button
      type={type}
      disabled={disabled}
      onClick={onClick}
      className={`cursor-pointer hover:brightness-75 disabled:cursor-not-allowed disabled:opacity-50 ${buttonVariants[variant]} ${buttonColors[color]}`}
    >
      {children}
    </button>
  );
}
