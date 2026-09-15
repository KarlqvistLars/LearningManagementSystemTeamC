export interface Course {
    id: string;
    courseName: string;
    description: string;
    startDate: Date;
    endDate: Date;
    createdAt: Date;
}

export interface EnrollmentDto {
    studentId: string;
    enrollmentDate: Date;
    studentName: string;
}