import type { ApiResponse } from "../../../api/types";
import { apiFetch } from "../../../api/client";
import type { Course } from "../types";

export async function fetchCourses(): Promise<Course[]> {
    const result: ApiResponse<Course[]> = await apiFetch<Course[]>(`/courses`);
    if (!result.success) {
        throw new Error(result.error?.message || "Failed to fetch courses");
    }

    return result.data;
}