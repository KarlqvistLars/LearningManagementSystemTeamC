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
    city: string | null;
    postalCode: string | null;
    address: string | null;
    dateOfBirth: string | null;
    email: string;
    phoneNumber: string | null;
    roleId: string;
    isActive: boolean;
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

export async function createUser(data: {
  firstName: string;
  lastName: string;
  city: string | null;
  postalCode: string | null;
  address: string | null;
  dateOfBirth: string | null;
  email: string;
  phoneNumber: string | null;
  password: string;
  roleId: string;
}): Promise<User> {
  const result: ApiResponse<User> = await apiFetch<User>("/users", {
    method: "POST",
    body: JSON.stringify(data),
  });

  if (!result.success) {
    const error = new Error(result.error.message);

    Object.assign(error, {
      code: result.error.code,
      details: result.error.details,
    });

    throw error;
  }

  return result.data;
}
