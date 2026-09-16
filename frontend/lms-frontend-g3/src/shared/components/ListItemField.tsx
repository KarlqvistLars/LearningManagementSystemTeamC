import { Link } from "react-router";

interface ListItemFieldProps {
  label: string;
  value: string;
  className?: string;
  link?: string;
}

export function ListItemField({
  label,
  value,
  className = "",
  link,
}: ListItemFieldProps) {
  const content = (
    <p
      className="truncate text-lg font-medium text-primary-display-text"
      title={value}
    >
      {value}
    </p>
  );

  return (
    <div className={`flex min-w-0 flex-col gap-3 ${className}`}>
      <p className="text-xs text-primary-title-text">{label}</p>

      {link ? <Link to={link}>{content}</Link> : content}
    </div>
  );
}
