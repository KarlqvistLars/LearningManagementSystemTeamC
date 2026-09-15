import type { ApiResponse } from "../../../api/types";
import { apiFetch } from "../../../api/client";
import type { Course, EnrollmentDto } from "../types";
import type { User } from "../../users/types/types";

export async function fetchCourses(): Promise<Course[]> {
    const result: ApiResponse<Course[]> = await apiFetch<Course[]>(`/courses`);
    if (!result.success) {
        throw new Error(result.error?.message || "Failed to fetch courses");
    }

    return result.data;
}

export async function fetchCoursesByStudent(studentId: string): Promise<Course[]> {
    const result: ApiResponse<Course[]> = await apiFetch<Course[]>(`/student/${studentId}/courses`);
    if (!result.success) {
        throw new Error(result.error?.message || "Failed to fetch courses");
    }

    return result.data;
}

export async function fetchCourseById(courseId: string): Promise<Course> {
    const result: ApiResponse<Course> = await apiFetch<Course>(`/courses/${courseId}`);
    if (!result.success) {
        throw new Error(result.error?.message || "Failed to fetch course");
    }

    return result.data;
}

export async function editCourse(course: Course): Promise<Course> {
    const result: ApiResponse<Course> = await apiFetch<Course>(`/courses/${course.id}`, {
        method: "PUT",
        body: JSON.stringify(course),
    });
    if (!result.success) {
        throw new Error(result.error?.message || "Failed to edit course");
    }
    return result.data;
}

export async function createCourse(course: Course): Promise<Course> {
    const result: ApiResponse<Course> = await apiFetch<Course>(`/courses`, {
        method: "POST",
        body: JSON.stringify(course),
    });
    if (!result.success) {
        throw new Error(result.error?.message || "Failed to create course");
    }
    return result.data;
}

export async function enrollUserInCourse(courseId: string, userId: string): Promise<boolean> {
    const result: ApiResponse<boolean> = await apiFetch<boolean>(`/courses/${courseId}/enroll/${userId}`, {
        method: "POST",
    });
    if (!result.success) {
        throw new Error(result.error?.message || "Failed to enroll user in course");
    }
    return result.data;
}

export async function fetchEnrollmentsByCourse(courseId: string): Promise<EnrollmentDto[]> {
    const result: ApiResponse<EnrollmentDto[]> = await apiFetch<EnrollmentDto[]>(`/courses/${courseId}/enrollments`);
    if (!result.success) {
        throw new Error(result.error?.message || "Failed to fetch enrollments for course");
    }
    return result.data;
}

export async function fetchMentorsByCourse(courseId: string): Promise<User[]> {
    const result: ApiResponse<User[]> = await apiFetch<User[]>(`/courses/${courseId}/mentors`);
    if (!result.success) {
        throw new Error(result.error?.message || "Failed to fetch mentors for course");
    }
    return result.data;
}