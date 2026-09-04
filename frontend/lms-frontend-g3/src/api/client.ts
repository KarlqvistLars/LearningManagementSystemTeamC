import type { ApiResponse } from "./types";

const API_URL = "http://localhost:5094/api";

export async function apiFetch<T>(
    endpoint: string,
    options?: RequestInit
): Promise<ApiResponse<T>> {
    const response = await fetch(`${API_URL}${endpoint}`, {
        ...options,
        headers: {
            "Content-Type": "application/json",
            ...options?.headers,
        },
    });

    return await response.json();
}