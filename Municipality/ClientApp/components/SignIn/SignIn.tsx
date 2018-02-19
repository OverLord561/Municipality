import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import autobind from 'autobind-decorator';

import { ApplicationState, IValidationError } from '../../store';
import { IState, ILoginModel } from './logic/signInState';
import * as actions from './logic/signInActions';

interface IStateProps {
  loginModel: ILoginModel;
  authorized: boolean;
  userName: string;
  errors: IValidationError[];
  isFetching: boolean;

}

const dispatchProps = {
  authorize: actions.Authorize,
  setFormData: actions.SetFormData,
};

type IProps = IState & IStateProps & RouteComponentProps<{}> & typeof dispatchProps;


class SignIn extends React.Component<IProps, any> {

  constructor(props: IProps) {
    super(props);
  }





  @autobind
  setLoginData(event: React.FormEvent<HTMLInputElement>) {
    const prop: string = event.currentTarget.dataset['prop'];
    const value: string = event.currentTarget.value;
    if (prop === 'rememberMe') {
      this.props.setFormData(prop, !this.props.loginModel.rememberMe);
    } else {
      return this.props.setFormData(prop, value);
    }
  }

  @autobind
  logIn(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    this.props.authorize(() => {
      this.props.history.goBack();
    });
  }

  public render() {
    return <div className="sign-in">
      <form className="form-horizontal" onSubmit={this.logIn}>
        <div className="form-group">
          <label className="control-label col-sm-2" htmlFor="email">Email:</label>
          <div className="col-sm-10">
            <input
              data-prop="email"
              value={this.props.loginModel.email} type="email" className="form-control" id="email" placeholder="Enter email" onChange={this.setLoginData} />
          </div>
        </div>
        <div className="form-group">
          <label className="control-label col-sm-2" htmlFor="pwd">Password:</label>
          <div className="col-sm-10">
            <input
              data-prop="password"
              value={this.props.loginModel.password} type="password" className="form-control" id="pwd" placeholder="Enter password" onChange={this.setLoginData} />
          </div>
        </div>
        <div className="form-group">
          <div className="col-sm-offset-2 col-sm-10">
            <div className="checkbox">
              <label><input
                data-prop="rememberMe"
                type="checkbox" onChange={this.setLoginData} checked={this.props.loginModel.rememberMe} /> Remember me</label>
            </div>
          </div>
        </div>
        <div className="form-group">
          <div className="col-sm-offset-2 col-sm-10">
            <button type="submit" className="btn btn-default">Submit</button>
          </div>
        </div>

        <div className="form-group has-error">
          <div className="col-sm-2">
          </div>
          <div className="col-sm-10">
            {this.props.errors.map((error, index) => {
              return <span key={index} className="help-block">{error.errorMessage}</span>;
            })}
          </div>

        </div>

      </form>
    </div>

  }
}

function mapStateToProps(state: ApplicationState): IStateProps {
  console.log(state);
  return {
    loginModel: state.signIn.loginModel,
    authorized: state.signIn.authorized,
    userName: state.signIn.userName,
    errors: state.signIn.errors,
    isFetching: state.signIn.isFetching,

  };
};


export default connect(
  mapStateToProps, // Selects which state properties are merged into the component's props
  dispatchProps                 // Selects which action creators are merged into the component's props
)(SignIn) as typeof SignIn;





