export interface Module {
    id: string;
    moduleName: string;
    description: string;
    startDate: Date;
    endDate: Date;
    courseId: string;
}

export interface CreateModule {
    name: string;
    description: string;
    startDate: Date;
    endDate: Date;
    courseId:string;
}

export interface EditModule {
    id: string;
    name: string;
    description: string;
    startDate: Date;
    endDate: Date;
    courseId: string;
}