import * as types from './signInConstants';
import { IState } from './signInState';
import { ApplicationState } from '../../../store';
import axios from 'axios';


const SetData = (user: IState) => {
    return {
        type: types.AUTHORIZE,
        user
    }
};


export const Authorize = (data: IState, goToPrevPage: any) => {

    let URL = '/api/sign-in/';
    return (dispatch: any, getStore: any) => {
        let state: IState = getStore().signUp;
        console.log(state)
        //return axios.post(URL,state)       
        return axios.post(URL, data)
            .then(response => {
                if (response.status === 200) {
                    data.authorized = true;
                    data.userName = data.email;
                    dispatch(SetData(data));
                    goToPrevPage();
                }
            }).catch(error => {
                console.log(error);
            });
    };
};

export const LogOut = () => {

    let URL = '/api/sign-out/';
    return (dispatch: any, getStore: any) => {
       
      
        
        return axios.post(URL)
            .then(response => {
                if (response.status === 200) {
                    window.location.href = '/';
                }
            }).catch(error => {
                console.log(error);
            });
    };
}


