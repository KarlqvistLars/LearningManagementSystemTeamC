import { apiFetch } from "../../../api/client";
import type { Module } from "../types";

export async function fetchModules(courseId: string, userId: string, role: string): Promise<Module[]> {
    const res = await apiFetch<Module[]>(`/modules/${courseId}?userId=${userId}&role=${role}`);
    if (!res.success) {
        throw new Error(res.error.message);
    }
    return res.data;
}