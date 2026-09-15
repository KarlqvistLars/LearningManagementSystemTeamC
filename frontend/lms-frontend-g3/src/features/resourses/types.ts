export interface ResourceDto {
  id: string;
  resourceName: string;
  content: string;
  url: string | null;
  createdDate: string;
  type: number;
  createdBy: string;
}

export interface ResourceWithCreatorDto {
  id: string;
  resourceName: string;
  content: string;
  url: string | null;
  createdDate: string;
  type: number;
  createdBy: string;
  createdByFirstName: string;
  createdByLastName: string;
}

export interface CreateResource {
  resourceName: string;
  content: string;
  url: string;
  createdDate: string;
  type: number;
  createdBy: string;
}

export interface ResourceTypeOption {
  value: number;
  name: string;
}