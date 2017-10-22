import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import { IState } from './logic/signUpState';
import * as actions from './logic/signUpActions';
import * as CounterStore from '../../store/Counter';
import autobind from 'autobind-decorator';

type IProps = IState & RouteComponentProps<{}> & typeof dispatchProps;

const dispatchProps = {
    register: actions.Register
};


class SignUp extends React.Component<IProps, any> {
    constructor(props: IProps) {
        super(props);
        this.state = {
            password: this.props.password,
            email: this.props.email,
            confirmPassword: this.props.confirmPassword
        }
    }

    componentDidMount() {

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
    SetConfirmPassword(event: React.FormEvent<HTMLInputElement>) {
        this.setState({
            confirmPassword: event.currentTarget.value
        });
    }

    @autobind
    SubmitForm(event: any) {
        event.preventDefault();

        this.props.register(this.state, this.props.history.goBack());
    }

    public render() {
        //console.log(this.props.history.goBack())
        console.log(this.props)
        return <div className="sign-up">
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
                    <label className="control-label col-sm-2" htmlFor="conf">Confirm password:</label>
                    <div className="col-sm-10">
                        <input value={this.state.confirmPassword} type="password" className="form-control" id="conf" placeholder="Confirm password" onChange={this.SetConfirmPassword} />
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
        email: state.signUp.email,
        password: state.signUp.password,
        confirmPassword: state.signUp.confirmPassword
    };
};


export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    dispatchProps                 // Selects which action creators are merged into the component's props
)(SignUp) as typeof SignUp;





