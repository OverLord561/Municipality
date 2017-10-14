import * as WeatherForecasts from './WeatherForecasts';
import * as Counter from './Counter';
import * as SignUp from '../components/SignUp/logic/signUpState';
import * as SignUpReducer from '../components/SignUp/logic/signUpReducer';
import * as Incidents from '../components/Incidents/logic/incidentsState';
import * as IncidentsReducer from '../components/Incidents/logic/incidentsReducer';


// The top-level state object
export interface ApplicationState {
    counter: Counter.CounterState;
    weatherForecasts: WeatherForecasts.WeatherForecastsState;
    signUp: SignUp.IState;
    incidents: Incidents.IState;

}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    counter: Counter.reducer,
    weatherForecasts: WeatherForecasts.reducer,
    signUp: SignUpReducer.ordersReducer,
    incidents: IncidentsReducer.incidentsReducer
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
