import type { ApiResponse } from "../../../api/types";
import { apiFetch } from "../../../api/client";
import type { Module, CreateModule, EditModule } from "../types";

// Fetches modules based on id
export async function fetchModulesById(id: string): Promise<Module> {
    const result: ApiResponse<Module> = await apiFetch<Module>(`/modules/${id}`);
    if (!result.success) {
        throw new Error(result.error?.message || "Failed to fetch module");
    }
    
    return result.data;
}

// Fetches modules by course id
export async function fetchModules(courseId: string): Promise<Module[]> {
    const result: ApiResponse<Module[]> = await apiFetch<Module[]>(`/modules/course/${courseId}`);
    if (!result.success) {
        throw new Error(result.error?.message || "Failed to fetch modules");
    }

    return result.data;
}

// Creates a module
export async function createModule(module: CreateModule): Promise<Module> {
    const result: ApiResponse<Module> = await apiFetch<Module>(`/modules`,
        {
            method: "POST",
            body: JSON.stringify(module),
        }
    );
    if (!result.success) {
        throw new Error(result.error?.message || "Failed to create module");
    }

    return result.data;
}

// Edits a module
export async function editModule(module: EditModule): Promise<Module> {
    const result: ApiResponse<Module> = await apiFetch<Module>(`/modules`,
        {
            method: "PUT",
            body: JSON.stringify(module),
        }
    );
    if (!result.success) {
        throw new Error(result.error?.message || "Failed to edit module");
    }

    return result.data;
}