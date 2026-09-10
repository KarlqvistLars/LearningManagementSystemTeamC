export type ButtonVariant = "form" | "list";

export type ButtonColor = "create" | "edit" | "delete" | "cancel" | "resource";

interface ButtonProps {
  children: React.ReactNode;
  type?: "button" | "submit" | "reset";
  disabled?: boolean;
  variant?: ButtonVariant;
  color?: ButtonColor;
}

const buttonVariants: Record<ButtonVariant, string> = {
  form: "w-full rounded-md px-4 py-3 font-medium",
  list: "rounded-md px-2 py-1 text-sm",
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
}: ButtonProps) {
  return (
    <button
      type={type}
      disabled={disabled}
      className={`cursor-pointer hover:brightness-75 disabled:cursor-not-allowed disabled:opacity-50 ${buttonVariants[variant]} ${buttonColors[color]}`}
    >
      {children}
    </button>
  );
}
