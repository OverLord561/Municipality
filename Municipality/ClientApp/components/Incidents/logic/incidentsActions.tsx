import * as types from './incidentsConstants';
import { IState } from './incidentsState';
import { ApplicationState } from '../../../store';
import axios from 'axios';

export const GetIncidents = () => {

   
    let URL = '/api/incidents';

    return (dispatch: any, getStore: any) => {      
            
        return axios.get(URL)  
            .then(response => {
                console.log(response);                 
                }).catch(error => {
                    console.log(error);
                });
        };
    
}

