
export interface IState {
    incidents: IIncident[];
}

export interface IIncident {
    id: number;
    title: string;
    description: string;
    longitude: string;
    latitude: string;
    status: string;
    statusId: number;
}


export function getInitialState(): IState {

    return {
        incidents: [{
            id: 0,
            title: " ",
            description: " ",
            latitude: " ",
            longitude: " ",
            status: " ",
            statusId: 0
        }]

    }
}