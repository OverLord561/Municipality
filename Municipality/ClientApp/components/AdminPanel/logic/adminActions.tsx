import * as types from './adminConstants';
import { IState, IIncident, IPoint } from './adminState';
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

export const ReceiveIncedents = (notApprovedIncidents: IIncident[]) => {
    return {
        type: types.RECEIVE_INCIDENTS,
        notApprovedIncidents
    }
}





