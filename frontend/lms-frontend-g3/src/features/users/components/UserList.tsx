import type { User } from "../types/types";
import { UserListItem } from "./UserListItem";

interface UserListProps {
  users: User[];
}

export function UserList({ users }: UserListProps) {
  if (users.length === 0) {
    return <p>No users found.</p>;
  }

  return (
    <div className="flex flex-col gap-2">
      {users.map((user) => (
        <UserListItem key={user.id} user={user} />
      ))}
    </div>
  );
}
