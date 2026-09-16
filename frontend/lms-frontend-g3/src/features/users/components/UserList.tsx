import type { User } from "../types/types";
import { UserListItem } from "./UserListItem";
import { DisplayText } from "../../../shared/components/DisplayText";
import { useState } from "react";
import type { ApiError } from "../../../api/types";
import { ErrorList } from "../../../shared/components/ErrorList";

interface UserListProps {
  users: User[];
  onToggleStatus: (userId: string) => void;
}

export function UserList({ users, onToggleStatus }: UserListProps) {
  if (users.length === 0) {
    return <DisplayText text="No users found." />;
  }

  const [error, setError] = useState<ApiError | undefined>();

  return (
    <div className="flex flex-col gap-2">
      <ErrorList error={error} />
      {users.map((user) => (
        <UserListItem
          key={user.id}
          user={user}
          onToggleStatus={onToggleStatus}
          onError={setError}
        />
      ))}
    </div>
  );
}
