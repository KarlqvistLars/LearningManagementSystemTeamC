import { Link } from "react-router/internal/react-server-client";

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
  return (
    <div className={`flex flex-col gap-3 ${className}`}>
      <p className="text-xs text-primary-title-text">{label}</p>

      {link ? (
        <Link to={link}>
          <p className="text-lg font-medium text-primary-display-text">
            {value}
          </p>
        </Link>
      ) : (
        <p className="text-lg font-medium text-primary-display-text">{value}</p>
      )}
    </div>
  );
}
