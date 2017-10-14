import * as types from './signUpConstants';
import { IState } from './signUpState';
import { ApplicationState } from '../../../store';
import axios from 'axios';

export const Authorize = () => {

   // let URL = `/api/sign-up/`;
    let URL = 'api/account/authorized-user';
    return (dispatch: any, getStore: any) => {
        let state: IState = getStore().signUp;
        console.log(state)
        //return axios.post(URL,state)       
        return axios.get(URL)  
            .then(response => {
                console.log(response);                 
                }).catch(error => {
                    console.log(error);
                });
        };
    
}

