import type { User } from "../types/types";

interface UserListItemProps {
  user: User;
}

export function UserListItem({ user }: UserListItemProps) {
  return (
    <div className="flex items-center justify-between rounded-xl border border-border bg-menu px-4 py-3">
      <div>
        <p className="font-medium text-primary-display-text">
          {user.firstName} {user.lastName}
        </p>

        <p className="text-sm text-secondary-display-text">{user.email}</p>
      </div>

      <span className="text-sm text-secondary-display-text">
        {user.roleName}
      </span>
    </div>
  );
}
