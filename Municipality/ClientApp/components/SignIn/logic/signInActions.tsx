import * as types from './signInConstants';
import { IState } from './signInState';
import { ApplicationState } from '../../../store';
import axios from 'axios';

export const Authorize = (data: IState, goToPrevPage: any) => {
   

    let URL = '/api/sign-in/';
    return (dispatch: any, getStore: any) => {
        let state: IState = getStore().signUp;
        console.log(state)
        //return axios.post(URL,state)       
        return axios.post(URL,data)  
            .then(response => {
                if (response.status === 200) {
                    goToPrevPage();
                }                
                }).catch(error => {
                    console.log(error);
                });
        };
    
}

