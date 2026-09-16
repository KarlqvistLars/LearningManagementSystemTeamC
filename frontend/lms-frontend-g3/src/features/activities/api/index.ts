import { apiFetch } from "../../../api/client";
import { apiRequest } from "../../../api/request";
import type { ActivityDto, ActivityDetailsDto } from "../types";
import type { AssignmentSubmissionDto } from "../types";
import type { CreateActivity, EditActivity } from "../types";

export async function getActivitiesByModule(
  moduleId: string,
): Promise<ActivityDto[]> {
  const res = await apiFetch<ActivityDto[]>(`/modules/${moduleId}/activities`);
  if (!res.success) {
    throw new Error(res.error.message);
  }
  return res.data;
}

export async function createActivity(
  activity: CreateActivity,
): Promise<ActivityDto> {
  return apiRequest<ActivityDto>("/activities", {
    method: "POST",
    body: JSON.stringify(activity),
  });
}

export async function editActivity(
  activity: EditActivity,
): Promise<ActivityDto> {
  return apiRequest<ActivityDto>(`/activities/${activity.id}`, {
    method: "PUT",
    body: JSON.stringify(activity),
  });
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
