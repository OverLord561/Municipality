
export interface IState {
    incidents: IIncident[];
}

export interface IIncident {
    id: number;
    title: string;
    description: string;
    lng: string;
    lat: string;
    status: string;
    statusId: number;
    adress: string;
    inFocus: boolean;
    approved: boolean;
    filePath: string;
    timeLeft: string;
    files: File[];
    [key: string]: string | number | boolean | File[];
}

export interface IPoint {
    lat: number;
    lng: number;
}

const Dnipropetrovsk: IPoint = { lat: 48.460861, lng: 35.056737 };

export const getInitialState = (): IState => {

    return {
        incidents: [],
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
        filePath: '',
        id: 0,
        inFocus: false,
        status: '',
        statusId: 0,
        timeLeft: '',
        files: [],
    };
};