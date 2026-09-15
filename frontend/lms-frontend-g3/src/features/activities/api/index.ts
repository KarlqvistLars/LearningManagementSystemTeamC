import { apiFetch } from "../../../api/client";
import { apiRequest } from "../../../api/request";
import type { ActivityDto, CreateActivity, EditActivity } from "../types";

export async function getActivitiesByModule(moduleId: string): Promise<ActivityDto[]> {
    const res = await apiFetch<ActivityDto[]>(`/modules/${moduleId}/activities`);
    if (!res.success) {
        throw new Error(res.error.message);
    }
    return res.data;
}

export async function createActivity(
    moduleId: string,
    activity: CreateActivity,
): Promise<ActivityDto> {
    return apiRequest<ActivityDto>(`/modules/${moduleId}/activities`, {
        method: "POST",
        body: JSON.stringify(activity),
    });
}

export async function editActivity(
    moduleId: string,
    activityId: string,
    activity: EditActivity,
): Promise<ActivityDto> {
    return apiRequest<ActivityDto>(`/modules/${moduleId}/activities/${activityId}`, {
        method: "PUT",
        body: JSON.stringify(activity),
    });
}