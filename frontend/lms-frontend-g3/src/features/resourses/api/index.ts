import { apiFetch } from "../../../api/client";
import type { ResourceDto } from "../types";

export async function getResourcesByActivity(activityId: string): Promise<ResourceDto[]> {
    const res = await apiFetch<ResourceDto[]>(`/activities/${activityId}/resources`);
    if (!res.success) {
        throw new Error(res.error.message);
    }
    return res.data;
}