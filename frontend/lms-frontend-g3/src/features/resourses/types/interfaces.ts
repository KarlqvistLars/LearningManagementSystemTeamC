export interface ResourceDto {
    id: string;
    resourceName: string;
    content: string;
    url: string;
    createdDate: Date;
    type: number;
    createdBy: string;
}

export interface ResourceWithCreatorDto {
    id: string;
    resourceName: string;
    content: string;
    url: string;
    createdDate: Date;
    type: number;
    createdBy: string;
}

export interface CreateResource {
    resourceName: string;
    content: string;
    url: string;
    createdDate: Date;
    type: number;
    createdBy: string;
}

export interface EditResource {
    id: string;
    resourceName: string;
    content: string;
    url: string;
    createdDate: Date;
    type: number;
    createdBy: string;
}