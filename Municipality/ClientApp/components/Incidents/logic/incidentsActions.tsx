import * as types from './incidentsConstants';
import { IState, IIncident, IPoint } from './incidentsState';
import { ApplicationState } from '../../../store';
import axios from 'axios';

export const GetIncidents = () => {


    let URL = '/api/incidents';

    return (dispatch: any, getStore: any) => {

        return axios.get(URL)
            .then(response => {
                dispatch(ReceiveIncedents(response.data.items));
            }).catch(error => {
                console.log(error);
            });
    };

}

export const ReceiveIncedents = (incidents: IIncident[]) => {
    return {
        type: types.RECEIVE_INCIDENTS,
        incidents
    }
}



export const CreateIncidents = (incident: any) => {
    return (dispatch: any, getStore: any) => {
        const config = { headers: { 'Content-Type': 'multipart/form-data' } };       

        return axios.post('/api/incident', incident)
            .then(response => {
                if (response.status == 200) {
                    dispatch(ReceiveIncedents(response.data.items));
                }
            }).catch(error => {
                console.log(error);
            });
    }
}

export const FocusIncident = (Id: number, value: boolean) => {
    return (dispatch: any, getStore: any) => {
        var copy: IIncident[] =getStore().incidents.incidents.slice();
            

        copy.filter(function (incident, index) {
            if (incident.id === Id) incident.inFocus = value;
        });
        
        return dispatch(ReceiveIncedents(copy));
    }
}