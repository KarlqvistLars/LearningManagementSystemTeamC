import { apiFetch } from "../../../api/client";
import type { Module } from "../types";

export async function fetchModules(courseId: string): Promise<Module[]> {
    const res = await apiFetch<Module[]>(`/modules/${courseId}`);
    if (!res.success) {
        throw new Error(res.error.message);
    }
    return res.data;
}