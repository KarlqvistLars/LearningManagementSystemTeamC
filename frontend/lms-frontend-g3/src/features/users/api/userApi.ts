import { apiRequest } from "../../../api/request";
import type { User } from "../types/types";

export async function getUsers(): Promise<User[]> {
  return apiRequest<User[]>("/users");
}

export async function getUserById(userId: string): Promise<User> {
  return apiRequest<User>(`/users/${userId}`);
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
  return apiRequest<User>(`/users/${userId}`, {
    method: "PUT",
    body: JSON.stringify(data),
  });
}

export async function toggleUserStatus(userId: string): Promise<void> {
  await apiRequest<string>(`/users/${userId}/status`, {
    method: "PATCH",
  });
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
  return apiRequest<User>("/users", {
    method: "POST",
    body: JSON.stringify(data),
  });
}
