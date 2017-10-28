
export interface IState {
    notApprovedIncidents: IIncident[];
    
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
}

export interface IPoint {
    lat: number;
    lng: number;
}

const Dnipropetrovsk: IPoint = { lat: 48.460861, lng: 35.056737 };

export function getInitialState(): IState {

    return {
        notApprovedIncidents: [{
            id: 0,
            title: " ",
            description: " ",
            lat: " ",
            lng: " ",
            status: " ",            
            statusId: 0,
            adress: " ",
            inFocus: false,
            approved: false,
            filePath:" "
        }]
    }
}