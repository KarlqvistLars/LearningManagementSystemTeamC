import { apiFetch } from "../../../api/client";
import type { ActivityDto } from "../types";

export async function getActivitiesByModule(moduleId: string): Promise<ActivityDto[]> {
    const res = await apiFetch<ActivityDto[]>(`/modules/${moduleId}/activities`);
    if (!res.success) {
        throw new Error(res.error.message);
    }
    return res.data;
}
