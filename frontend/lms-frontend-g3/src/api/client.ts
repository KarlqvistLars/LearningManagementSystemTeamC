import type { ApiResponse } from "./types";

const API_URL = "https://localhost:7001/api";

export async function apiFetch<T>(
  endpoint: string,
  options?: RequestInit,
): Promise<ApiResponse<T>> {
  const token = localStorage.getItem("access_token");

  const response = await fetch(`${API_URL}${endpoint}`, {
    ...options,
    headers: {
      "Content-Type": "application/json",
      ...(token
        ? {
            Authorization: `Bearer ${token}`,
          }
        : {}),
      ...options?.headers,
    },
  });

  return await response.json();
}
