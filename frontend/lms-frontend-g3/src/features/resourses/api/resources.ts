import type { ApiResponse } from "../../../api/types";
import { apiFetch } from "../../../api/client";
import type { ResourceDto, CreateResource } from "../types/interfaces";
import type { ResourceTypeOption } from "../types/interfaces";

// Fetches modules based on id
export async function fetchResourcesById(id: string): Promise<ResourceDto> {
  const result: ApiResponse<ResourceDto> = await apiFetch<ResourceDto>(
    `/resources/${id}`,
  );
  if (!result.success) {
    throw new Error(result.error?.message || "Failed to fetch resource");
  }

  return result.data;
}

// Fetches resources by activity id
export async function fetchResources(
  activityId: string,
): Promise<ResourceDto[]> {
  const result: ApiResponse<ResourceDto[]> = await apiFetch<ResourceDto[]>(
    `/resources/activity/${activityId}`,
  );
  if (!result.success) {
    throw new Error(result.error?.message || "Failed to fetch resources");
  }

  return result.data;
}

// Creates a module
export async function createResource(
  resource: CreateResource,
): Promise<ResourceDto> {
  const result: ApiResponse<ResourceDto> = await apiFetch<ResourceDto>(
    `/resources`,
    {
      method: "POST",
      body: JSON.stringify(resource),
    },
  );

  if (!result.success) {
    console.error("API response:", result);
    console.error(
      "Validation details:",
      JSON.stringify(result.error.details, null, 2),
    );
    console.log("Resource being sent:", resource);

    throw new Error(result.error.message || "Failed to create resource");
  }

  // if (!result.success) {
  //     console.error("API response:", result);
  //     console.error("Validation errors:", result?.error);
  //     console.log("Resource being sent:", JSON.stringify(resource));
  //     throw new Error(result.error?.message || "Failed to create resource");
  // }

  return result.data;
}

// Edits a resource
export async function editResource(
  resource: ResourceDto,
): Promise<ResourceDto> {
  const result: ApiResponse<ResourceDto> = await apiFetch<ResourceDto>(
    `/resources`,
    {
      method: "PUT",
      body: JSON.stringify(resource),
    },
  );
  if (!result.success) {
    throw new Error(result.error?.message || "Failed to edit resource");
  }

  return result.data;
}

export async function getResourceTypes(): Promise<ResourceTypeOption[]> {
  const result = await apiFetch<ResourceTypeOption[]>("/resources/types");

  if (!result.success) {
    throw new Error(result.error.message || "Failed to load resource types");
  }

  return result.data;
}
