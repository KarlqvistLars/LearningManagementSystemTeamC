export interface Course {
    id: string;
    courseName: string;
    description: string;
    startDate: string;
    endDate: string;
    createdAt: string;
}

export interface CourseDto {
    courseName: string;
    description: string;
    startDate: string;
    endDate: string;
}

export interface EnrollmentDto {
    studentId: string;
    enrollmentDate: string;
    studentName: string;
}