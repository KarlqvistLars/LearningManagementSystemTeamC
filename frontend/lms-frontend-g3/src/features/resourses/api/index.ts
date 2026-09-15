import { apiFetch } from "../../../api/client";
import type { Resource } from "../types/interfaces";

export async function getResourcesByActivity(resourceId: string): Promise<Resource[]> {
    const res = await apiFetch<Resource[]>(`/activities/${resourceId}/resources`);
    if (!res.success) {
        throw new Error(res.error.message);
    }
    return res.data;
}