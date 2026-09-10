import { useEffect, useState } from "react";
import { SearchInput } from "../../../shared/components/SearchInput";
import { DisplayText } from "../../../shared/components/DisplayText";
import { UserList } from "../components/UserList";
import { getUsers } from "../api/userApi";
import type { User } from "../types/types";

export function UserPage() {
  const [searchTerm, setSearchTerm] = useState("");
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    async function fetchUsers() {
      try {
        const users = await getUsers();
        setUsers(users);
      } catch (error) {
        console.error(error);
      }
    }

    fetchUsers();
  }, []);

  const search = searchTerm.toLowerCase();

  const filteredUsers = users.filter((user) => {
    return (
      user.firstName.toLowerCase().includes(search) ||
      user.lastName.toLowerCase().includes(search) ||
      user.email.toLowerCase().includes(search)
    );
  });

  return (
    <section className="flex flex-col gap-6 p-6">
      <DisplayText text="USERS" />

      <SearchInput
        value={searchTerm}
        onChange={setSearchTerm}
        placeholder="Search users..."
      />

      <UserList users={filteredUsers} />
    </section>
  );
}
