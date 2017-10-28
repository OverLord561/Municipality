import { getInitialState, IState } from './incidentsState';
import * as types from './incidentsConstants';

type KnownAction = any;

declare interface IReducer<TState> {
    (state: TState, action: KnownAction): TState;
}

const initialState = getInitialState();

export const incidentsReducer: IReducer<IState> = (state = initialState, action) => {  

    switch (action.type) {
               

        case types.RECEIVE_INCIDENTS: {
            let newstate = Object.assign({}, state, {
                incidents: action.incidents               
            });

            return newstate;
        }
        case types.SET_CURRENT_POSITION: {
            let newstate = Object.assign({}, state, {
                center: action.center
            });
            return newstate;

        };
    }

    return getInitialState();
} 