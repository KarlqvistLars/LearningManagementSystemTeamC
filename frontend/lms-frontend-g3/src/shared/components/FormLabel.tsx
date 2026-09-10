interface FormLabelProps {
  htmlFor: string;
  children: React.ReactNode;
}

export function FormLabel({ htmlFor, children }: FormLabelProps) {
  return (
    <label htmlFor={htmlFor} className="mb-1 block text-lg font-medium">
      {children}
    </label>
  );
}
