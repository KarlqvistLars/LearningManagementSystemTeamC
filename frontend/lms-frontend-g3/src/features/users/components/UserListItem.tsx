import type { User } from "../types/types";
import { ListItemField } from "../../../shared/components/ListItemField";
import { Tag } from "../../../shared/components/Tag";
import type { TagVariant } from "../../../shared/components/Tag";

interface UserListItemProps {
  user: User;
}

export function UserListItem({ user }: UserListItemProps) {
  return (
    <div className="flex items-center rounded-xl border border-border bg-menu px-4 py-3">
      <ListItemField
        label="Name"
        value={`${user.firstName} ${user.lastName}`}
        className="flex-2"
      />

      <Tag label={user.roleName} variant={user.roleName as TagVariant} />

      <ListItemField
        label="Email"
        value={user.email}
        className="min-w-0 flex-3"
      />

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
