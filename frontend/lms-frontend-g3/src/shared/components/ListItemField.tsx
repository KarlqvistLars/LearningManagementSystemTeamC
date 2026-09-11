interface ListItemFieldProps {
  label: string;
  value: string;
  className?: string;
}

export function ListItemField({
  label,
  value,
  className = "",
}: ListItemFieldProps) {
  return (
    <div className={`flex flex-col gap-3 ${className}`}>
      <p className="text-xs text-primary-title-text">{label}</p>

      <p className="text-lg font-medium text-primary-display-text">{value}</p>
    </div>
  );
}
