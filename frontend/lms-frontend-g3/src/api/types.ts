export type ApiResponse<T> =
    | {
        success: true;
        data: T;
        error?: never;
    }
    | {
        success: false;
        data?: never;
        error: ApiErrors[];
    };

export interface ApiErrors {
    code: string;
    message: string;
    details?: Record<string, string[]>;
}