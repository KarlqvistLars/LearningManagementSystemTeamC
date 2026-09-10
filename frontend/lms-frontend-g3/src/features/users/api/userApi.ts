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
