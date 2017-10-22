import { getInitialState, IState } from './signInState';
import * as types from './signInConstants';

type KnownAction = any;

declare interface IReducer<TState> {
    (state: TState, action: KnownAction): TState;
}

const initialState = getInitialState();

export const signInReducer: IReducer<IState> = (state = initialState, action) => {  

    switch (action.type) {
        case types.AUTHORIZE:
            return action.user;
    }
    return state;
} 