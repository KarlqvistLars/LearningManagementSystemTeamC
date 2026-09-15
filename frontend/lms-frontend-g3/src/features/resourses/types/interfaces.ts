export interface Resource {
    id: string;
    resourceName: string;
    content: string;
    url: string;
    createdAt: Date;
    type: number;
}

export interface CreateResource {
    resourceName: string;
    content: string;
    url: string;
    createdAt: Date;
    type: number;
}

export interface EditResource {
    id: string;
    resourceName: string;
    content: string;
    url: string;
    createdAt: Date;
    type: number;
}