import { apiRequest } from "../../../api/request";
import type { ResourceWithCreatorDto } from "../types";

export async function getResourcesByActivity(
  activityId: string,
): Promise<ResourceWithCreatorDto[]> {
  return apiRequest<ResourceWithCreatorDto[]>(
    `/activities/${activityId}/resources`,
  );
}

export async function getAllResources(): Promise<ResourceWithCreatorDto[]> {
  return apiRequest<ResourceWithCreatorDto[]>("/resources");
}
