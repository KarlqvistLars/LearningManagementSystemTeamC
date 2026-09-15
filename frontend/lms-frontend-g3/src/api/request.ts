import type { ApiResponse } from "./types";
import { apiFetch } from "./client";
import { ApiRequestError } from "./error";

export async function apiRequest<T>(
  endpoint: string,
  options?: RequestInit,
): Promise<T> {
  const result: ApiResponse<T> = await apiFetch<T>(endpoint, options);

  if (!result.success) {
    throw new ApiRequestError(result.error);
  }

  return result.data;
}
