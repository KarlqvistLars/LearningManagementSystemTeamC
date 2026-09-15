import { apiFetch } from "../../../api/client";
import { apiRequest } from "../../../api/request";
import type { ResourceDto, ResourceTypeOption, ResourceWithCreatorDto } from "../types";

export async function getResourceById(
  resourceId: string,
): Promise<ResourceWithCreatorDto> {
  return apiRequest<ResourceWithCreatorDto>(`/resources/${resourceId}`);
}

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
  console.log(`Resource created with name ${resource.resourceName}.`);
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
  console.log(`Resource with ID ${resourceId} updated.`);
  return apiRequest<ResourceDto>(`/resources/${resourceId}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(resource),
  });
}

export async function getResourceTypes(): Promise<
  ResourceTypeOption[]
> {
  const result =
    await apiFetch<ResourceTypeOption[]>("/resources/types");

  if (!result.success) {
    throw new Error(
      result.error.message || "Failed to load resource types"
    );
  }

  return result.data;
}