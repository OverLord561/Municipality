import axios from 'axios';

import * as types from './signUpConstants';
import { IState, IRegisterModel } from './signUpState';
import { ApplicationState } from '../../../store';
import * as globalConstants from '../../../constants/constants';

export const Register = (goToPrevPage: any) => (dispatch: any, getStore: any) => {

  let URL = '/api/sign-up/';
  // const config = { headers: { 'Content-Type': "application/json;charset=utf-8" } };
  // return axios.post(URL,state) 
  const data: IRegisterModel = getStore().signUp.registerModel;

  dispatch({
    type: globalConstants.IS_FETCHING,
    isFetching: true,
  });
  axios.post(URL, data)
    .then(response => {
      dispatch({
        type: globalConstants.IS_FETCHING,
        isFetching: false,
      });
      goToPrevPage();
    }).catch(error => {
      dispatch({
        type: globalConstants.IS_FETCHING,
        isFetching: false,
      });
      console.log(error);
      if (error.response.status === 409) {
        dispatch({
          type: globalConstants.ADD_VALIDATION_ERROR,
          errors: error.response.data,
        });
      }
    });
};

export const SetFormData = (property: string, value: string) => (dispatch: any, getStore: any) => {
  const model: IRegisterModel = { ...getStore().signUp.registerModel };
  model[property] = value;
  dispatch({
    type: types.SET_REGISTER_DATA,
    registerModel: model,
  });
};



