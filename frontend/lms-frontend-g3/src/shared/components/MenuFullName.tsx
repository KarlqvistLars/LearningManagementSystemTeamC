interface MenuFullNameProps {
  firstName: string;
  lastName: string;
}

export function MenuFullName({ firstName, lastName }: MenuFullNameProps) {
  return (
    <span className="text-sm font-medium text-primary-display-text">
      {firstName} {lastName}
    </span>
  );
}
