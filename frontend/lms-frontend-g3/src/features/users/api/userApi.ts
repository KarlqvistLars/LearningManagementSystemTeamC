import type { ApiResponse } from "../../../api/types";
import { apiFetch } from "../../../api/client";
import type { User } from "../types/types";

export async function getUsers(): Promise<User[]> {
  const result: ApiResponse<User[]> = await apiFetch<User[]>("/users");

  if (!result.success) {
    throw new Error(result.error?.message || "Failed to fetch users");
  }

  return result.data;
}

export async function getUserById(userId: string): Promise<User> {
  const result: ApiResponse<User> = await apiFetch<User>(`/users/${userId}`);

  if (!result.success) {
    throw new Error(result.error?.message || "Failed to fetch user");
  }

  return result.data;
}

export async function updateUser(
  userId: string,
  data: {
    firstName: string;
    lastName: string;
    email: string;
    roleId: string;
  },
): Promise<User> {
  const result: ApiResponse<User> = await apiFetch<User>(`/users/${userId}`, {
    method: "PUT",
    body: JSON.stringify(data),
  });

  if (!result.success) {
    throw new Error(result.error?.message || "Failed to update user");
  }

  return result.data;
}

export async function toggleUserStatus(userId: string): Promise<void> {
  const result: ApiResponse<string> = await apiFetch<string>(
    `/users/${userId}/status`,
    {
      method: "PATCH",
    },
  );

  if (!result.success) {
    throw new Error(result.error?.message || "Failed to update user status");
  }
}
