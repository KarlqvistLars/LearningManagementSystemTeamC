import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { SearchInput } from "../../../shared/components/SearchInput";
import { DisplayText } from "../../../shared/components/DisplayText";
import { UserList } from "../components/UserList";
import { toggleUserStatus, getUsers } from "../api/userApi";
import type { User } from "../types/types";
import { Button } from "../../../shared/components/Button";

export function UserPage() {
  const [searchTerm, setSearchTerm] = useState("");
  const [users, setUsers] = useState<User[]>([]);
  const navigate = useNavigate();
  const fetchUsers = async () => {
    try {
      const users = await getUsers();
      setUsers(users);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  const handleToggleStatus = async (userId: string) => {
    try {
      await toggleUserStatus(userId);
      await fetchUsers();
    } catch (error) {
      console.error(error);
    }
  };

  const search = searchTerm.toLowerCase();

  const filteredUsers = users.filter((user) => {
    return (
      user.firstName.toLowerCase().includes(search) ||
      user.lastName.toLowerCase().includes(search) ||
      user.email.toLowerCase().includes(search)
    );
  });

  return (
    <section className="flex flex-col gap-6 p-6 h-full">
      <DisplayText text="USERS" />

      <SearchInput
        value={searchTerm}
        onChange={setSearchTerm}
        placeholder="Search users..."
      />

      <UserList users={filteredUsers} onToggleStatus={handleToggleStatus} />

      <div className="self-center mt-auto">
        <Button
          type="button"
          children="Create new user"
          variant="list"
          color="create"
          onClick={() => navigate("/users/create")}
        />
      </div>
    </section>
  );
}
