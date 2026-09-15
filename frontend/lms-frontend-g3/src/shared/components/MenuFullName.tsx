import { Link } from "react-router";

interface MenuFullNameProps {
  userId: string;
  firstName: string;
  lastName: string;
}

export function MenuFullName({
  userId,
  firstName,
  lastName,
}: MenuFullNameProps) {
  return (
    <Link
      to={`/users/${userId}/edit`}
      className="text-sm font-medium text-primary-display-text"
    >
      {firstName} {lastName}
    </Link>
  );
}
