import type { User } from "../types/types";
import { ListItemField } from "../../../shared/components/ListItemField";
import { Tag } from "../../../shared/components/Tag";
import type { TagVariant } from "../../../shared/components/Tag";
import { Button } from "../../../shared/components/Button";

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

      <Tag
        title="Role"
        label={user.roleName}
        variant={user.roleName as TagVariant}
        className="flex-1"
      />

      <ListItemField
        label="Email"
        value={user.email}
        className="min-w-0 flex-2"
      />

      <div className="flex items-center gap-1">
        <Button children="Edit" variant="list" color="edit" />
        <Button children="Delete" variant="list" color="delete" />
      </div>
    </div>
  );
}
