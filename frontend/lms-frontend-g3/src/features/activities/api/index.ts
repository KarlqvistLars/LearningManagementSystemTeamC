import { apiFetch } from "../../../api/client";
import type { ActivityDto } from "../types";

export async function getActivitiesByModule(moduleId: string, userId: string, role: string): Promise<ActivityDto[]> {
    const res = await apiFetch<ActivityDto[]>(`/modules/${moduleId}/activities?userId=${userId}&role=${role.toUpperCase()}`);
    if (!res.success) {
        throw new Error(res.error.message);
    }
    return res.data;
}