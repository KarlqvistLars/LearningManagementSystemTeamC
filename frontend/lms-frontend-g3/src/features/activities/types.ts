export interface ActivityDto {
    id: string;
    activityName: string;
    type: number;
    description: string;
    startDate: string;
    endDate: string;
    moduleId: string;
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
