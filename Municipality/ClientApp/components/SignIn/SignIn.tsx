import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import { IState } from './logic/signInState';
import * as actions from './logic/signInActions';
import * as CounterStore from '../../store/Counter';
import autobind from 'autobind-decorator';


type IProps = IState & RouteComponentProps<{}> & typeof dispatchProps;

const dispatchProps = {
    authorize: actions.Authorize
};


class SignIn extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);
        this.state = {
            password: this.props.password,
            email: this.props.email,
            rememberMe: this.props.rememberMe
        }
    }



    @autobind
    SetEmail(event: React.FormEvent<HTMLInputElement>) {
        this.setState({
            email: event.currentTarget.value
        });
    }

    @autobind
    SetPassword(event: React.FormEvent<HTMLInputElement>) {
        this.setState({
            password: event.currentTarget.value
        });
    }
    @autobind
    RememberMe() {
        this.setState({
            rememberMe: !this.state.rememberMe
        });
    }



    @autobind
    SubmitForm(event: any) {
        event.preventDefault();
        this.props.authorize(this.state, this.props.history.goBack);
    }

    public render() {
        return <div className="sign-in">
            <form className="form-horizontal" onSubmit={this.SubmitForm}>
                <div className="form-group">
                    <label className="control-label col-sm-2" htmlFor="email">Email:</label>
                    <div className="col-sm-10">
                        <input value={this.state.email} type="email" className="form-control" id="email" placeholder="Enter email" onChange={this.SetEmail} />
                    </div>
                </div>
                <div className="form-group">
                    <label className="control-label col-sm-2" htmlFor="pwd">Password:</label>
                    <div className="col-sm-10">
                        <input value={this.state.password} type="password" className="form-control" id="pwd" placeholder="Enter password" onChange={this.SetPassword} />
                    </div>
                </div>
                <div className="form-group">
                    <div className="col-sm-offset-2 col-sm-10">
                        <div className="checkbox">
                            <label><input type="checkbox" onChange={this.RememberMe} /> Remember me</label>
                        </div>
                    </div>
                </div>
                <div className="form-group">
                    <div className="col-sm-offset-2 col-sm-10">
                        <button type="submit" className="btn btn-default">Submit</button>
                    </div>
                </div>
            </form>
        </div>

    }
}

function mapStateToProps(state: ApplicationState): IState {
    return {
        email: state.signIn.email,
        password: state.signIn.password,
        rememberMe: state.signIn.rememberMe
    };
};


export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    dispatchProps                 // Selects which action creators are merged into the component's props
)(SignIn) as typeof SignIn;





