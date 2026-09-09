import type { ApiResponse } from "../../../api/types";
import { apiFetch } from "../../../api/client";
import type { LoginResult } from "../types";

export async function login(
  email: string,
  password: string,
): Promise<LoginResult> {
  const result: ApiResponse<LoginResult> = await apiFetch<LoginResult>(
    "/auth/login",
    {
      method: "POST",
      body: JSON.stringify({
        email,
        password,
      }),
    },
  );

  if (!result.success) {
    throw new Error(result.error?.message || "Failed to login");
  }

  return result.data;
}
