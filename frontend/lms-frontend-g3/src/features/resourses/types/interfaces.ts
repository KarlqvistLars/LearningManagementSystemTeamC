export interface ResourceDto {
  id: string;
  resourceName: string;
  content: string;
  url: string | null;
  createdAt: string;
  type: string;
  createdBy: string;
}

export interface ResourceWithCreatorDto {
  id: string;
  resourceName: string;
  content: string;
  url: string | null;
  createdAt: string;
  type: string;
  createdBy: string;
  createdByFirstName: string;
  createdByLastName: string;
}

export interface CreateResource {
  resourceName: string;
  content: string;
  url: string | null;
  type: string;
  activityId: string | null;
}

export interface EditResource {
  resourceName: string;
  content: string;
  url: string | null;
  type: string;
}

export interface ResourceTypeOption {
  value: number;
  name: string;
}

export interface StudentSubmissionDto {
  id: string;
  resourceName: string;
  content: string;
  url: string | null;
  createdAt: string;
  type: string;
}
