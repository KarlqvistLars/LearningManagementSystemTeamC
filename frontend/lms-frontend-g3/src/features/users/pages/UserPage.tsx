import { useState } from "react";
import { SearchInput } from "../../../shared/components/SearchInput";
import { UserList } from "../components/UserList";
import type { User } from "../types/types";

export function UserPage() {
  const [searchTerm, setSearchTerm] = useState("");

  const users: User[] = [];

  const filteredUsers = users.filter((user) => {
    const search = searchTerm.toLowerCase();

    return (
      user.firstName.toLowerCase().includes(search) ||
      user.lastName.toLowerCase().includes(search) ||
      user.email.toLowerCase().includes(search)
    );
  });

  return (
    <section className="flex flex-col gap-6 p-6">
      <h1 className="text-2xl font-semibold text-primary-display-text">
        Users
      </h1>

      <SearchInput
        value={searchTerm}
        onChange={setSearchTerm}
        placeholder="Search users..."
      />

      <UserList users={filteredUsers} />
    </section>
  );
}
