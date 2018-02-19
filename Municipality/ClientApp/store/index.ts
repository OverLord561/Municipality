
import * as SignUp from '../components/SignUp/logic/signUpState';
import * as SignUpReducer from '../components/SignUp/logic/signUpReducer';
import * as SignIn from '../components/SignIn/logic/signInState';
import * as SignInReducer from '../components/SignIn/logic/signInReducer';
import * as Incidents from '../components/Incidents/logic/incidentsState';
import * as IncidentsReducer from '../components/Incidents/logic/incidentsReducer';
import * as Admin from '../components/AdminPanel/logic/adminState';
import * as AdminReducer from '../components/AdminPanel/logic/adminReducer';

// The top-level state object
export interface ApplicationState {
  signUp: SignUp.IState;
  incidents: Incidents.IState;
  signIn: SignIn.IState;
  admin: Admin.IState;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
  signUp: SignUpReducer.signUpReducer,
  incidents: IncidentsReducer.incidentsReducer,
  signIn: SignInReducer.signInReducer,
  admin: AdminReducer.adminReducer
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
  (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}

export interface IModel {
  isFetching: boolean;
  errors: IValidationError[];
}

export interface IValidationError {
  exception: string;
  errorMessage: string;
}
