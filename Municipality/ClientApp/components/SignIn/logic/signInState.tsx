﻿import { IModel, IValidationError } from '../../../store/index';

export interface IState extends IModel {
  loginModel: ILoginModel;
  authorized: boolean;
  userName: string;
}

export interface ILoginModel {
  email: string;
  password: string;
  rememberMe: boolean;
  [key: string]: string | boolean;
}

export function getInitialState(): IState {

  return {
    loginModel: {
      email: "yurapuk452@gmail.com",
      password: "123Qaz-",
      rememberMe: false,
    },
    authorized: false,
    userName: "",
    errors: [],
    isFetching: false,
  };
}