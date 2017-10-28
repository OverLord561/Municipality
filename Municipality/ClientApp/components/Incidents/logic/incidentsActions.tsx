import * as types from './incidentsConstants';
import { IState, IIncident, IPoint } from './incidentsState';
import { ApplicationState } from '../../../store';
import axios from 'axios';

export const GetIncidents = () => {

    
    let URL = '/api/incidents';

    return (dispatch: any, getStore: any) => {
        
        return axios.get(URL)
            .then(response => {
                return dispatch(ReceiveIncedents(response.data.items));
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



export const CreateIncidents = (incident: FormData) => {
    return (dispatch: any, getStore: any) => {
        const config = { headers: { 'Content-Type': 'multipart/form-data' } };
        let lat: any = incident.get('lat');
        let lng: any = incident.get('lng');
       

        return axios.get(`https://maps.googleapis.com/maps/api/geocode/json?latlng=${lat},${lng}&key=AIzaSyAxVVyh7rh6kKCYhxyWZSY_xkDNZ4YIK3k`)
            .then(response => {
                if (response.status === 200) {
                    incident.append("adress", response.data.results[0].formatted_address);
                    console.log(incident.get('adress'))
                    return axios.post('/api/incidents', incident)
                        .then(response => {
                            if (response.status == 200) {
                                dispatch(ReceiveIncedents(response.data.items));
                            }
                        }).catch(error => {
                            console.log(error);
                        });

                }
            }).catch(error => {
                console.log(error);
            });
        
    }
}

export const GetStreet = (lat: string, lng: string): any => {
    return (dispatch: any, getStore: any) => {
        axios.get(`https://maps.googleapis.com/maps/api/geocode/json?latlng=${lat},${lng}&key=AIzaSyAxVVyh7rh6kKCYhxyWZSY_xkDNZ4YIK3k`)
            .then(response => {
                if (response.status === 200) {
                    return response.data.results[0].formatted_address;
                }
            }).catch(error => {
                console.log(error);
            });

    }
}


export const FocusIncident = (Id: number, value: boolean) => {
    return (dispatch: any, getStore: any) => {
        var copy: IIncident[] = getStore().incidents.incidents.slice();


        copy.filter(function (incident, index) {
            if (incident.id === Id) incident.inFocus = value;
        });

        return dispatch(ReceiveIncedents(copy));
    }
}