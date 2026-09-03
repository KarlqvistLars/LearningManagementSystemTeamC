export type ApiResponse<T> =
    | {
        success: true;
        data: T;
        error?: never;
    }
    | {
        success: false;
        data?: never;
        error: ApiError;
    };

export interface ApiError {
    code: string;
    message: string;
    details?: Record<string, string[]>;
}

export interface ActivityDto {
    id: string;
    activityName: string;
    type: number;
    description: string;
    startDate: string;
    endDate: string;
    moduleId: string;
    moduleName: string;
}