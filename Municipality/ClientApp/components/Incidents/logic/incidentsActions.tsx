import * as types from './incidentsConstants';
import { IState, IIncident } from './incidentsState';
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

