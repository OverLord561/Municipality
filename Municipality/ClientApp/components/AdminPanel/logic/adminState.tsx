import { IIncident } from '../../Incidents/logic/incidentsState';

export interface IState {
    notApprovedIncidents: IIncident[];
}



export interface IPoint {
    lat: number;
    lng: number;
}

const Dnipropetrovsk: IPoint = { lat: 48.460861, lng: 35.056737 };

export const getInitialState = (): IState => {

    return {
        notApprovedIncidents: [{
            id: 0,
            title: '',
            description: '',
            lat: '',
            lng: '',
            status: '',
            statusId: 0,
            adress: '',
            inFocus: false,
            approved: false,
            filePaths: [],
            priority: '',
            priorityId: 0,
            estimate: 1,
            files: [],
            timeLeft: ''
        }]
    };
};