import type { User } from "../types/types";

interface UserListItemProps {
  user: User;
}

export function UserListItem({ user }: UserListItemProps) {
  return (
    <div className="flex items-center rounded-xl border border-border bg-menu px-4 py-3">
      <div className="flex-2 flex flex-col gap-3">
        <p className="text-xs text-primary-title-text">Name</p>
        <p className="text-lg font-medium text-primary-display-text">
          {user.firstName} {user.lastName}
        </p>
      </div>

      <div className="flex-1 flex flex-col gap-3">
        <p className="text-xs text-primary-title-text">Role</p>
        <p className="text-lg font-medium text-primary-display-text">
          {user.roleName}
        </p>
      </div>

      <div className="min-w-0 flex-3 flex flex-col gap-3">
        <p className="text-xs text-primary-title-text">Email</p>
        <p className="truncate text-lg font-medium text-primary-display-text">
          {user.email}
        </p>
      </div>

      <div className="flex items-center gap-2">
        <button type="button" className="rounded-lg px-3 py-2 text-sm">
          Edit
        </button>

        <button type="button" className="rounded-lg px-3 py-2 text-sm">
          Delete
        </button>
      </div>
    </div>
  );
}
