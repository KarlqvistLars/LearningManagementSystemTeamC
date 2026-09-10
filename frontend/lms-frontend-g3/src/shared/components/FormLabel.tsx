interface FormLabelProps {
  htmlFor: string;
  children: React.ReactNode;
}

export function FormLabel({ htmlFor, children }: FormLabelProps) {
  return (
    <label htmlFor={htmlFor} className="mb-1 block text-base font-medium">
      {children}
    </label>
  );
}
