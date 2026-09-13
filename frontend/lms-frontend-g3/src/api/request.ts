import type { ApiResponse } from "./types";
import { apiFetch } from "./client";

export async function apiRequest<T>(
  endpoint: string,
  options?: RequestInit,
): Promise<T> {
  const result: ApiResponse<T> = await apiFetch<T>(endpoint, options);

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
