import { apiFetch } from "../../../api/client";
import type { ActivityDto, ActivityDetailsDto } from "../types";
import { apiRequest } from "../../../api/request";
import type { AssignmentSubmissionDto } from "../types";

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

export async function getAssignmentSubmissions(
  activityId: string,
): Promise<AssignmentSubmissionDto[]> {
  return apiRequest<AssignmentSubmissionDto[]>(
    `/activities/${activityId}/submissions`,
  );
}
