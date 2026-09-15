import { apiFetch } from "../../../api/client";
import type { ActivityDto, ActivityDetailsDto } from "../types";
import { apiRequest } from "../../../api/request";

export async function getActivitiesByModule(
  moduleId: string,
): Promise<ActivityDto[]> {
  const res = await apiFetch<ActivityDto[]>(`/modules/${moduleId}/activities`);
  if (!res.success) {
    throw new Error(res.error.message);
  }
  return res.data;
}

export async function getAssignments(): Promise<ActivityDetailsDto[]> {
  return apiRequest<ActivityDetailsDto[]>("/activities/assignments");
}
