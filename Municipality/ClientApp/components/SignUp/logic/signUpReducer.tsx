import { getInitialState, IState } from './signUpState';
import * as types from './signUpConstants';
import * as globalConstants from '../../../constants/constants';

type KnownAction = any;

declare interface IReducer<TState> {
  (state: TState, action: KnownAction): TState;
}

const initialState = getInitialState();

export const signUpReducer: IReducer<IState> = (state = initialState, action) => {

  switch (action.type) {
    case globalConstants.ADD_VALIDATION_ERROR: {
      return {
        ...state,
        errors: action.errors,
      };
    }
    case types.SET_REGISTER_DATA: {
      return {
        ...state,
        registerModel: action.registerModel,
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