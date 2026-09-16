export interface ActivityDto {
  id: string;
  activityName: string;
  type: number;
  description: string;
  startDate: string;
  endDate: string;
  moduleId: string;
}

export interface ActivityDetailsDto {
  id: string;
  activityName: string;
  type: number;
  description: string;
  startDate: string;
  endDate: string;
  moduleId: string;
  moduleName: string;
  courseId: string;
  courseName: string;
  submittedCount: number;
  totalStudents: number;
}

export interface AssignmentSubmissionDto {
  studentId: string;
  studentFirstName: string;
  studentLastName: string;

  activityId: string;
  activityName: string;
  endDate: string;

  submissionId: string | null;
  submittedAt: string | null;
}

export interface CreateActivity {
    activityName: string;
    description: string;
    startDate: string;
    endDate: string;
    type: string;
    moduleId: string;
}

export interface EditActivity {
    id: string;
    activityName: string;
    description: string;
    startDate: string;
    endDate: string;
    type: string;
    moduleId: string;
}
