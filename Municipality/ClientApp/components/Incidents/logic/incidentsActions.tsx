import * as types from './incidentsConstants';
import { IState, IIncident, IPoint } from './incidentsState';
import { ApplicationState } from '../../../store';
import axios from 'axios';

export const RequestIncidents = () => (dispatch: any, getStore: any) => {

    let URL = '/api/incidents?IsApproved=true';

    axios.get(URL)
        .then(response => {

            const totalPages: number = Math.ceil(response.data.totalCount / 5);

            // const totalPages: number = 5;
            const page: number = getStore().incidents.page;

            const listOfPages: number[] = CalculatePages(totalPages, page);

            dispatch({
                type: types.SET_LIST_OF_PAGES,
                listOfPages: listOfPages,
                totalPages: totalPages,
            });

            dispatch(ReceiveIncedents(response.data.items, response.data.totalCount));
        }).catch(error => {
            console.log(error);
        });
};



export const ReceiveIncedents = (incidents: IIncident[], totalCount: number = 0) => (dispatch: any, getStore: any) => {

    let count = totalCount;
    if (totalCount === 0) {
        count = getStore().incidents.totalCount;
    }
    return dispatch({
        type: types.RECEIVE_INCIDENTS,
        incidents: incidents,
        totalCount: count,
    });
}



export const CreateIncidents = (incident: IIncident, callback: Function) => {
    return (dispatch: any, getStore: any) => {
        let lat: any = incident.lat;
        let lng: any = incident.lng;


        GetStreet(lat, lng).then(
            (adress) => {
                incident.adress = adress;
            }
        );

        const form: FormData = new FormData();

        form.append('incident', JSON.stringify(incident));
        for (let i = 0; i < incident.files.length; i++) {
            form.append(incident.files[i].name, incident.files[i]);
        }

        return axios.post('/api/incidents', form)
            .then(response => {
                alert("request accepted");
            }).catch(error => {
                console.log(error);
            });
    }
}

export const GetStreet = (lat: string, lng: string) => {

    return axios.get(`https://maps.googleapis.com/maps/api/geocode/json?latlng=${lat},${lng}&key=AIzaSyAxVVyh7rh6kKCYhxyWZSY_xkDNZ4YIK3k`)
        .then(response => {
            return response.data.results[0].formatted_address;
        }).catch(error => {
            console.log(error);
            return '';
        });

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


export const CalculatePages = (
    totalPages: number,
    currentPage: number,
): number[] => {
    let listOfPages: number[] = [];
    if (totalPages <= 5) {
        for (let i = 1; i <= totalPages; i++) {
            listOfPages.push(i);
        }
    } else {
        if (currentPage <= 4) {
            listOfPages = [1, 2, 3, 4];
        } else if (currentPage > 4 && currentPage < totalPages) {
            listOfPages.push(currentPage - 2);
            listOfPages.push(currentPage - 1);
            listOfPages.push(currentPage);
            if (currentPage < totalPages) {
                listOfPages.push(currentPage + 1);
            }
        } else {
            for (let i = 3; i >= 0; i--) {
                listOfPages.push(totalPages - i);
            }
        }
    }

    return listOfPages;
};


