import { apiRequest } from "../../../api/request";
import type {
  ResourceDto,
  CreateResource,
  EditResource,
  ResourceTypeOption,
  ResourceWithCreatorDto,
  StudentSubmissionDto,
} from "../types/interfaces";

export async function getResourceById(
  resourceId: string,
): Promise<ResourceWithCreatorDto> {
  return apiRequest<ResourceWithCreatorDto>(`/resources/${resourceId}`);
}

export async function getResourcesByActivity(
  activityId: string,
): Promise<ResourceDto[]> {
  return apiRequest<ResourceDto[]>(`/activities/${activityId}/resources`);
}

export async function getAllResources(): Promise<ResourceDto[]> {
  return apiRequest<ResourceDto[]>("/resources");
}

export async function createResource(
  resource: CreateResource,
): Promise<ResourceDto> {
  return apiRequest<ResourceDto>("/resources", {
    method: "POST",
    body: JSON.stringify(resource),
  });
}

export async function updateResource(
  resourceId: string,
  resource: EditResource,
): Promise<ResourceDto> {
  return apiRequest<ResourceDto>(`/resources/${resourceId}`, {
    method: "PUT",
    body: JSON.stringify(resource),
  });
}

export async function getResourceTypes(): Promise<ResourceTypeOption[]> {
  return apiRequest<ResourceTypeOption[]>("/resources/types");
}

export async function getMySubmission(
  activityId: string,
): Promise<StudentSubmissionDto | null> {
  return apiRequest<StudentSubmissionDto | null>(
    `/activities/${activityId}/submission`,
  );
}
