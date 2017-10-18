import * as types from './signUpConstants';
import { IState } from './signUpState';
import { ApplicationState } from '../../../store';
import axios from 'axios';

export const Register = (data: IState, callback: any) => {
   
   // let URL = `/api/sign-up/`;
    let URL = '/api/sign-up/';
    const config = { headers: { 'Content-Type':  "application/json;charset=utf-8" } };
    return (dispatch: any, getStore: any) => {
        
        //return axios.post(URL,state)       
        return axios.post(URL, data)  
            .then(response => {
                if (response.status == 200) {
                    callback();                    
                }                 
                }).catch(error => {
                    console.log(error);
                });
        };
    
}

