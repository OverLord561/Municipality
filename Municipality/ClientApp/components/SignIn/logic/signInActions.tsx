import * as types from './signInConstants';
import { IState } from './signInState';
import { ApplicationState } from '../../../store';
import axios from 'axios';

export const Authorize = (data: IState) => {
   

    let URL = '/api/sign-in/';
    return (dispatch: any, getStore: any) => {
        let state: IState = getStore().signUp;
        console.log(state)
        //return axios.post(URL,state)       
        return axios.post(URL,data)  
            .then(response => {
                console.log(response);                 
                }).catch(error => {
                    console.log(error);
                });
        };
    
}

