
export interface IState {
    incidents: IIncident[];
    listOfPages: number[];
    page: number;
    totalCount: number;
    totalPages: number;
}

export interface IIncident {
    id: number;
    title: string;
    description: string;
    lng: string;
    lat: string;
    status: string;
    statusId: number;
    priority: string;
    priorityId: number;
    adress: string;
    inFocus: boolean;
    approved: boolean;
    filePaths: string[];
    timeLeft: string;
    files: File[];
    estimate: number;
    [key: string]: string | number | boolean | File[] | string[];
}

export interface IPoint {
    lat: number;
    lng: number;
}

const Dnipropetrovsk: IPoint = { lat: 48.460861, lng: 35.056737 };

export const getInitialState = (): IState => {

    return {
        incidents: [],
        listOfPages: [],
        page: 1,
        totalCount: 0,
        totalPages: 0,
    };
};

export const getIncidentCreateModel = (): IIncident => {
    return {
        title: 'test title',
        description: 'test description',
        lat: '0',
        lng: '0',
        adress: '',
        approved: false,
        filePaths: [],
        id: 0,
        inFocus: false,
        status: '',
        statusId: 0,
        priority: '',
        priorityId: 0,
        timeLeft: '',
        files: [],
        estimate: 0,
    };
};