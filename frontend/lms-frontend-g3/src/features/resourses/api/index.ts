import { apiRequest } from "../../../api/request";
import type { ResourceDto, ResourceWithCreatorDto } from "../types";

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

export async function createResource(
  resource: Omit<ResourceDto, "id" | "createdBy">,
): Promise<ResourceDto> {
  return apiRequest<ResourceDto>("/resources", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(resource),
  });
}

export async function updateResource(
  resourceId: string,
  resource: Omit<ResourceDto, "id" | "createdBy">,
): Promise<ResourceDto> {
  return apiRequest<ResourceDto>(`/resources/${resourceId}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(resource),
  });
}
