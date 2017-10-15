﻿import { getInitialState, IState } from './incidentsState';
import * as types from './incidentsConstants';

type KnownAction = any;

declare interface IReducer<TState> {
    (state: TState, action: KnownAction): TState;
}

const initialState = getInitialState();

export const incidentsReducer: IReducer<IState> = (state = initialState, action) => {  

    switch (action.type) {
        case types.REQUEST_INCIDENTS:
            console.log(action.param)
    }
    return state;
} 