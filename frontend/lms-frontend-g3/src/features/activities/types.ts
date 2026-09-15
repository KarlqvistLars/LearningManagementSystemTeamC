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
