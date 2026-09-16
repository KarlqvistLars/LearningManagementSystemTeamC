import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { SearchInput } from "../../../shared/components/SearchInput";
import { DisplayText } from "../../../shared/components/DisplayText";
import { UserList } from "../components/UserList";
import { toggleUserStatus, getUsers } from "../api/userApi";
import type { User } from "../types/types";
import { Button } from "../../../shared/components/Button";
import { useAuth } from "../../auth/AuthContext";
import type { ApiError } from "../../../api/types";
import { ApiRequestError } from "../../../api/error";
import { ErrorList } from "../../../shared/components/ErrorList";

export function UserPage() {
  const [searchTerm, setSearchTerm] = useState("");
  const [users, setUsers] = useState<User[]>([]);
  const [error, setError] = useState<ApiError | undefined>();

  const navigate = useNavigate();
  const { isTeacher } = useAuth();
  const fetchUsers = async () => {
    try {
      const users = await getUsers();
      setUsers(users);
    } catch (error) {
      if (error instanceof ApiRequestError) {
        setError(error);
        return;
      }
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
      if (error instanceof ApiRequestError) {
        setError(error);
        return;
      }
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
    <section className="flex flex-col gap-6 p-6 min-h-full">
      <DisplayText text="USERS" />

      <SearchInput
        value={searchTerm}
        onChange={setSearchTerm}
        placeholder="Search users..."
      />
      <ErrorList error={error} variant="list" />
      <UserList users={filteredUsers} onToggleStatus={handleToggleStatus} />

      {isTeacher && (
        <div className="self-center mt-auto">
          <Button
            type="button"
            children="Create new user"
            variant="list"
            color="create"
            onClick={() => navigate("/users/create")}
          />
        </div>
      )}
    </section>
  );
}
