import type { InputHTMLAttributes } from "react";

export function FormInput({
  className = "",
  ...props
}: InputHTMLAttributes<HTMLInputElement>) {
  return (
    <input
      {...props}
      className={`w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text ${className}`}
    />
  );
}
