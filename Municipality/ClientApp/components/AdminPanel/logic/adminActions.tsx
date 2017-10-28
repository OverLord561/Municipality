import * as types from './adminConstants';
import { IState, IIncident, IPoint } from './adminState';
import { ApplicationState } from '../../../store';
import axios from 'axios';

export const GetIncidents = () => {


    let URL = '/api/incidents/not-approved';

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


export const ApproveIncident = (id:number) => {

    let URL = `/api/incidents/${id}/approve`;

    return (dispatch: any, getStore: any) => {

        return axios.put(URL)
            .then(response => {
                if (response.status == 200) {

                    var copy: IIncident[] = [ ...getStore().admin.notApprovedIncidents ];
                    console.log(copy);
                    var incidents: IIncident[] = copy.filter(function (_incident, index) {
                        if (_incident.id != id) {
                            return _incident;
                        }
                    });
                    console.log(incidents);
                    console.log(copy);
                    
                    dispatch(ReceiveIncedents(incidents));
                }
            }).catch(error => {
                console.log(error);
            });
    };
}

export const ForbidIncident = (id: number) => {

    let URL = `/api/incidents/${id}`;

    return (dispatch: any, getStore: any) => {

        return axios.delete(URL)
            .then(response => {
                if (response.status == 200) {

                    var copy: IIncident[] = [...getStore().admin.notApprovedIncidents];
                   
                    var incidents: IIncident[] = copy.filter(function (_incident, index) {
                        if (_incident.id != id) {
                            return _incident;
                        }
                    });
                    

                    dispatch(ReceiveIncedents(incidents));
                }
            }).catch(error => {
                console.log(error);
            });
    };
}






