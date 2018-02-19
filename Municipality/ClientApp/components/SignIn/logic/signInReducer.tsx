import { getInitialState, IState } from './signInState';
import * as types from './signInConstants';
import * as globalConstants from '../../../constants/constants';
type KnownAction = any;

declare interface IReducer<TState> {
  (state: TState, action: KnownAction): TState;
}

const initialState = getInitialState();

export const signInReducer: IReducer<IState> = (state = initialState, action) => {

  switch (action.type) {
    case globalConstants.ADD_VALIDATION_ERROR: {
      return {
        ...state,
        errors: action.errors,
      };
    }
    case types.SET_LOGIN_DATA: {
      return {
        ...state,
        loginModel: action.loginModel,
      };
    }
    case globalConstants.IS_FETCHING: {
      return {
        ...state,
        isFetching: action.isFetching,
      };
    }
    default: { return state };
  }
} 