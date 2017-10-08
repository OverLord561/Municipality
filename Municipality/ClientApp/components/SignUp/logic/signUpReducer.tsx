import { getInitialState, IState } from './signUpState';
import * as types from './signUpConstants';

type KnownAction = any;

declare interface IReducer<TState> {
    (state: TState, action: KnownAction): TState;
}

const initialState = getInitialState();

export const ordersReducer: IReducer<IState> = (state = initialState, action) => {  

    switch (action.type) {
        case types.AUTHORIZE:
            console.log(action.param)
    }
    return state;
} 