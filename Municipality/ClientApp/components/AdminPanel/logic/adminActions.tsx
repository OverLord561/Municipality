import * as types from './adminConstants';
import { IState, IPoint } from './adminState';
import { IIncident } from '../../Incidents/logic/incidentsState';
import { ApplicationState } from '../../../store';
import axios from 'axios';

export const RequestIncidents = () => (dispatch: any, getStore: any) => {

    let URL = '/api/incidents?page=2';
    axios.get(URL)
        .then(response => {
            dispatch(ReceiveIncedents(response.data.items));
        }).catch(error => {
            console.log(error);
        });
};



export const ReceiveIncedents = (notApprovedIncidents: IIncident[]) => {
    return {
        type: types.RECEIVE_INCIDENTS,
        notApprovedIncidents: notApprovedIncidents,
    };
};


export const ApproveIncident = (incident: IIncident) => {

    let URL = `/api/incidents/${incident.id}/approve`;

    return (dispatch: any, getStore: any) => {

        return axios.put(URL, incident)
            .then(response => {
                if (response.status == 200) {

                    var copy: IIncident[] = [...getStore().admin.notApprovedIncidents];
                    console.log(copy);
                    var incidents: IIncident[] = copy.filter(function (_incident, index) {
                        if (_incident.id != incident.id) {
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






