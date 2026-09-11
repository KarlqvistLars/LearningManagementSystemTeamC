interface FormLabelProps {
  htmlFor: string;
  children: React.ReactNode;
  className?: string;
}

export function FormLabel({
  htmlFor,
  children,
  className = "",
}: FormLabelProps) {
  return (
    <label
      htmlFor={htmlFor}
      className={`mb-1 block text-base font-medium ${className}`}
    >
      {children}
    </label>
  );
}
