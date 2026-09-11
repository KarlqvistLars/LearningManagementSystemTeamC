import type { ApiResponse } from "../../../api/types";
import { apiFetch } from "../../../api/client";
import type { Role } from "../types/role";

export async function getRoles(): Promise<Role[]> {
  const result: ApiResponse<Role[]> = await apiFetch<Role[]>("/roles");

  if (!result.success) {
    throw new Error(result.error?.message || "Failed to fetch roles");
  }

  return result.data;
}
