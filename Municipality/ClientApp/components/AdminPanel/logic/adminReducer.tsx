﻿import { getInitialState, IState } from './adminState';
import * as types from './adminConstants';

type KnownAction = any;

declare interface IReducer<TState> {
    (state: TState, action: KnownAction): TState;
}

const initialState = getInitialState();

export const adminReducer: IReducer<IState> = (state = initialState, action) => {  

    console.log(action)
    switch (action.type) {
        
        case types.RECEIVE_INCIDENTS: {
          
            let newstate = Object.assign({}, state, {
                notApprovedIncidents: action.notApprovedIncidents               
            });
          
            return newstate;
        }
       
    }
    return state;
} 