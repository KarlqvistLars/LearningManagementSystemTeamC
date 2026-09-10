import type { User } from "../types/types";
import { UserListItem } from "./UserListItem";
import { DisplayText } from "../../../shared/components/DisplayText";

interface UserListProps {
  users: User[];
  onDelete: (userId: string) => void;
}

export function UserList({ users, onDelete }: UserListProps) {
  if (users.length === 0) {
    return <DisplayText text="No users found." />;
  }

  return (
    <div className="flex flex-col gap-2">
      {users.map((user) => (
        <UserListItem key={user.id} user={user} onDelete={onDelete} />
      ))}
    </div>
  );
}
