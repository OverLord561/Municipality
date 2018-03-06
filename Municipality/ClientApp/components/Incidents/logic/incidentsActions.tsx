import * as types from './incidentsConstants';
import { IState, IIncident, IPoint } from './incidentsState';
import { ApplicationState } from '../../../store';
import axios from 'axios';

export const GetIncidents = () => (dispatch: any, getStore: any) => {

    let URL = '/api/incidents?IsApproved=true';

    axios.get(URL)
        .then(response => {
            return dispatch(ReceiveIncedents(response.data.items));
        }).catch(error => {
            console.log(error);
        });
};



export const ReceiveIncedents = (incidents: IIncident[]) => {
    return {
        type: types.RECEIVE_INCIDENTS,
        incidents
    }
}



export const CreateIncidents = (incident: IIncident, callback: Function) => {
    return (dispatch: any, getStore: any) => {
        let lat: any = incident.lat;
        let lng: any = incident.lng;


        return axios.get(`https://maps.googleapis.com/maps/api/geocode/json?latlng=${lat},${lng}&key=AIzaSyAxVVyh7rh6kKCYhxyWZSY_xkDNZ4YIK3k`)
            .then(response => {
                if (response.status === 200) {
                    incident.adress = response.data.results[0].formatted_address;

                    const form: FormData = new FormData();

                    form.append('incident', JSON.stringify(incident));
                    for (let i = 0; i < incident.files.length; i++) {
                        form.append(incident.files[i].name, incident.files[i]);
                    }

                    return axios.post('/api/incidents', form)
                        .then(response => {
                            alert("request accepted");
                            dispatch(ReceiveIncedents(response.data.items));
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